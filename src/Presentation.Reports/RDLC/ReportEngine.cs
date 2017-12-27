using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Platform.Presentation.Reports.RDLC
{
    public static class ReportEngine
    {
        #region Initialize

        public static Stream GenerateReport(ReportBuilder reportBuilder)
        {
            Stream ret = new MemoryStream(Encoding.UTF8.GetBytes(GetReportData(reportBuilder)));
            return ret;
        }

        private static ReportBuilder InitAutoGenerateReport(ReportBuilder reportBuilder)
        {
            if (reportBuilder != null && reportBuilder.DataSource != null && reportBuilder.DataSource.Tables.Count > 0)
            {
                System.Data.DataSet ds = reportBuilder.DataSource;

                int _TablesCount = ds.Tables.Count;
                ReportBuilder.ReportTable[] reportTables = new ReportBuilder.ReportTable[_TablesCount];

                if (reportBuilder.AutoGenerateReport)
                {
                    for (int j = 0; j < _TablesCount; j++)
                    {
                        System.Data.DataTable dt = ds.Tables[j];
                        ReportBuilder.ReportColumns[] columns = new ReportBuilder.ReportColumns[dt.Columns.Count];
                        ReportBuilder.ReportScale ColumnScale = new ReportBuilder.ReportScale();
                        ColumnScale.Width = 4;
                        ColumnScale.Height = 1;
                        ReportBuilder.ReportDimensions ColumnPadding = new ReportBuilder.ReportDimensions();
                        ColumnPadding.Default = 2;
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            columns[i] = new ReportBuilder.ReportColumns() { ColumnCell = new ReportBuilder.ReportTextBoxControl() { Name = dt.Columns[i].ColumnName, Size = ColumnScale, Padding = ColumnPadding }, HeaderText = dt.Columns[i].ColumnName, HeaderColumnPadding = ColumnPadding };
                        }

                        reportTables[j] = new ReportBuilder.ReportTable() { ReportName = dt.TableName, ReportDataColumns = columns };
                    }
                }
                reportBuilder.Body = new ReportBuilder.ReportBody();
                reportBuilder.Body.ReportControlItems = new ReportBuilder.ReportItems();
                reportBuilder.Body.ReportControlItems.ReportTable = reportTables;
            }
            return reportBuilder;
        }

        private static string GetReportData(ReportBuilder reportBuilder)
        {
            reportBuilder = InitAutoGenerateReport(reportBuilder);
            string rdlcXML = "";
            rdlcXML += @"<?xml version=""1.0"" encoding=""utf-8""?>
                        <Report xmlns=""http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition""
                        xmlns:rd=""http://schemas.microsoft.com/SQLServer/reporting/reportdesigner"">
                      <Body>";

            string _tableData = GenerateTable(reportBuilder);

            if (_tableData.Trim() != "")
            {
                rdlcXML += @"<ReportItems>" + _tableData + @"</ReportItems>";
            }
            byte[] imgBinary = File.ReadAllBytes(reportBuilder.Logo);
            rdlcXML += @"<Height>2.1162cm</Height>
                        <Style />
                      </Body>
                      <Width>20.8cm</Width>
                      <Page>
                        " + GetPageHeader(reportBuilder) + GetFooter(reportBuilder) + GetReportPageSettings() + @"
                        <Style />
                      </Page>
                      <AutoRefresh>0</AutoRefresh>
                        " + GetDataSet(reportBuilder) + @"
                      <EmbeddedImages>
                        <EmbeddedImage Name=""Logo"">
                          <MIMEType>image/png</MIMEType>
                          <ImageData>" + System.Convert.ToBase64String(imgBinary) + @"</ImageData>
                        </EmbeddedImage>
                      </EmbeddedImages>
                      <Language>it-IT</Language>
                      <ConsumeContainerWhitespace>true</ConsumeContainerWhitespace>
                      <rd:ReportUnitType>Cm</rd:ReportUnitType>
                      <rd:ReportID>17efa4a3-5c39-4892-a44b-fbde95c96585</rd:ReportID>
                    </Report>";
            return rdlcXML;
        }

        #endregion Initialize

        #region Page Settings

        private static string GetReportPageSettings()
        {
            return @" <PageHeight>21cm</PageHeight>
    <PageWidth>29.5cm</PageWidth>
    <LeftMargin>0.1cm</LeftMargin>
    <RightMargin>0.1cm</RightMargin>
    <TopMargin>0.1cm</TopMargin>
    <BottomMargin>0.1cm</BottomMargin>
    <ColumnSpacing>1cm</ColumnSpacing>";
        }

        private static string GetPageHeader(ReportBuilder reportBuilder)
        {
            string strHeader = "";
            if (reportBuilder.Page == null || reportBuilder.Page.ReportHeader == null) return "";
            ReportBuilder.ReportSections reportHeader = reportBuilder.Page.ReportHeader;
            strHeader = @"<PageHeader>
                          <Height>" + reportHeader.Size.Height.ToString() + @"in</Height>
                          <PrintOnFirstPage>" + reportHeader.PrintOnFirstPage.ToString().ToLower() + @"</PrintOnFirstPage>
                          <PrintOnLastPage>" + reportHeader.PrintOnLastPage.ToString().ToLower() + @"</PrintOnLastPage>
                          <ReportItems>";
            ReportBuilder.ReportTextBoxControl[] headerTxt = reportBuilder.Page.ReportHeader.ReportControlItems.TextBoxControls;
            if (headerTxt != null)
                for (int i = 0; i < headerTxt.Count(); i++)
                {
                    strHeader += GetTextBox(headerTxt[i].Name, null, headerTxt[i].ValueOrExpression);
                }
            strHeader += @"
                            <Image Name=""Image1"">
                              <Source>Embedded</Source>
                              <Value>Logo</Value>
                              <Sizing>FitProportional</Sizing>
                              <Top>0.05807in</Top>
                              <Left>0.06529in</Left>
                              <Height>0.4375in</Height>
                              <Width>1.36459in</Width>
                              <ZIndex>1</ZIndex>
                              <Style />
                            </Image>
                          </ReportItems>
                          <Style />
                        </PageHeader>";
            return strHeader;
        }

        private static string GetFooter(ReportBuilder reportBuilder)
        {
            string strFooter = "";
            if (reportBuilder.Page == null || reportBuilder.Page.ReportFooter == null) return "";
            strFooter = @"<PageFooter>
                          <Height>0.68425in</Height>
                          <PrintOnFirstPage>true</PrintOnFirstPage>
                          <PrintOnLastPage>true</PrintOnLastPage>
                          <ReportItems>";
            ReportBuilder.ReportTextBoxControl[] footerTxt = reportBuilder.Page.ReportFooter.ReportControlItems.TextBoxControls;
            if (footerTxt != null)
                for (int i = 0; i < footerTxt.Count(); i++)
                {
                    strFooter += GetTextBox(footerTxt[i].Name, null, footerTxt[i].ValueOrExpression);
                }
            strFooter += @"</ReportItems>
                          <Style />
                        </PageFooter>";
            return strFooter;
        }

        #endregion Page Settings

        #region Dataset

        private static string GetDataSet(ReportBuilder reportBuilder)
        {
            string dataSetStr = "";
            if (reportBuilder != null && reportBuilder.DataSource != null && reportBuilder.DataSource.Tables.Count > 0)
            {
                string dsName = "rptCustomers";
                dataSetStr += @"<DataSources>
    <DataSource Name=""" + dsName + @""">
      <ConnectionProperties>
        <DataProvider>System.Data.DataSet</DataProvider>
        <ConnectString>/* Local Connection */</ConnectString>
      </ConnectionProperties>
      <rd:DataSourceID>944b21fd-a128-4363-a5fc-312a032950a0</rd:DataSourceID>
    </DataSource>
  </DataSources>
  <DataSets>"
                             + GetDataSetTables(reportBuilder.Body.ReportControlItems.ReportTable, dsName) +
                  @"</DataSets>";
            }
            return dataSetStr;
        }

        private static string GetDataSetTables(ReportBuilder.ReportTable[] tables, string DataSourceName)
        {
            string strTables = "";
            for (int i = 0; i < tables.Length; i++)
            {
                strTables += @"<DataSet Name=""" + tables[i].ReportName + @""">
      <Query>
        <DataSourceName>" + DataSourceName + @"</DataSourceName>
        <CommandText>/* Local Query */</CommandText>
      </Query>
     " + GetDataSetFields(tables[i].ReportDataColumns) + @"
    </DataSet>";
            }
            return strTables;
        }

        private static string GetDataSetFields(ReportBuilder.ReportColumns[] reportColumns)
        {
            string strFields = "";

            strFields += @"<Fields>";
            for (int i = 0; i < reportColumns.Length; i++)
            {
                strFields += @"<Field Name=""" + reportColumns[i].ColumnCell.Name + @""">
          <DataField>" + reportColumns[i].ColumnCell.Name + @"</DataField>
          <rd:TypeName>System.String</rd:TypeName>
        </Field>";
            }
            strFields += @"</Fields>";
            return strFields;
        }

        #endregion Dataset

        #region Report Table Configuration

        private static string GenerateTable(ReportBuilder reportBuilder)
        {
            string TableStr = "";
            if (reportBuilder != null && reportBuilder.DataSource != null && reportBuilder.DataSource.Tables.Count > 0)
            {
                ReportBuilder.ReportTable table = new ReportBuilder.ReportTable();
                for (int i = 0; i < reportBuilder.Body.ReportControlItems.ReportTable.Length; i++)
                {
                    table = reportBuilder.Body.ReportControlItems.ReportTable[i];
                    TableStr += @"<Tablix Name=""table_" + table.ReportName + @""">
        <TablixBody>
          " + GetTableColumns(reportBuilder, table) + @"
          <TablixRows>
            " + GenerateTableHeaderRow(reportBuilder, table) + GenerateTableRow(reportBuilder, table) + @"
          </TablixRows>
        </TablixBody>" + GetTableColumnHeirarchy(reportBuilder, table) + @"
        <TablixRowHierarchy>
          <TablixMembers>
            <TablixMember>
              <KeepWithGroup>After</KeepWithGroup>
            </TablixMember>
            <TablixMember>
              <Group Name=""" + table.ReportName + "_Details" + @""" />
            </TablixMember>
          </TablixMembers>
        </TablixRowHierarchy>
        <RepeatColumnHeaders>true</RepeatColumnHeaders>
        <RepeatRowHeaders>true</RepeatRowHeaders>
        <DataSetName>" + table.ReportName + @"</DataSetName>" + GetSortingDetails(reportBuilder) + @"
        <Top>0.07056cm</Top>
        <Left>0cm</Left>
        <Height>1.2cm</Height>
        <Width>7.5cm</Width>
        <Style>
          <Border>
            <Style>None</Style>
          </Border>
        </Style>
      </Tablix>";
                }
            }
            return TableStr;
        }

        private static string GetSortingDetails(ReportBuilder reportBuilder)
        {
            //return "";
            ReportBuilder.ReportTable[] tables = reportBuilder.Body.ReportControlItems.ReportTable;
            ReportBuilder.ReportColumns[] columns = reportBuilder.Body.ReportControlItems.ReportTable[0].ReportDataColumns;
            ReportBuilder.ReportTextBoxControl sortColumn = new ReportBuilder.ReportTextBoxControl();
            if (columns == null) return "";

            string strSorting = "";
            strSorting = @" <SortExpressions>";
            for (int i = 0; i < columns.Length; i++)
            {
                sortColumn = columns[i].ColumnCell;
                strSorting += "<SortExpression><Value>=Fields!" + sortColumn.Name + @".Value</Value>";
                if (columns[i].SortDirection == ReportBuilder.ReportSort.Descending)
                    strSorting += "<Direction>Descending</Direction>";
                strSorting += @"</SortExpression>";
            }
            strSorting += "</SortExpressions>";
            return strSorting;
        }

        private static string GenerateTableRow(ReportBuilder reportBuilder, ReportBuilder.ReportTable table)
        {
            ReportBuilder.ReportColumns[] columns = table.ReportDataColumns;
            ReportBuilder.ReportTextBoxControl ColumnCell = new ReportBuilder.ReportTextBoxControl();
            ReportBuilder.ReportScale colHeight = ColumnCell.Size;
            ReportBuilder.ReportDimensions padding = new ReportBuilder.ReportDimensions();
            if (columns == null) return "";

            string strTableRow = "";
            strTableRow = @"<TablixRow>
                <Height>0.6cm</Height>
                <TablixCells>";
            for (int i = 0; i < columns.Length; i++)
            {
                ColumnCell = columns[i].ColumnCell;
                padding = ColumnCell.Padding;
                strTableRow += @"<TablixCell>
                  <CellContents>
                   " + GenerateTextBox("txtCell_" + table.ReportName + "_", ColumnCell.Name, "", true, padding) + @"
                  </CellContents>
                </TablixCell>";
            }
            strTableRow += @"</TablixCells></TablixRow>";
            return strTableRow;
        }

        private static string GenerateTableHeaderRow(ReportBuilder reportBuilder, ReportBuilder.ReportTable table)
        {
            ReportBuilder.ReportColumns[] columns = table.ReportDataColumns;
            ReportBuilder.ReportTextBoxControl ColumnCell = new ReportBuilder.ReportTextBoxControl();
            ReportBuilder.ReportDimensions padding = new ReportBuilder.ReportDimensions();
            if (columns == null) return "";

            string strTableRow = "";
            strTableRow = @"<TablixRow>
                <Height>0.6cm</Height>
                <TablixCells>";
            for (int i = 0; i < columns.Length; i++)
            {
                ColumnCell = columns[i].ColumnCell;
                padding = columns[i].HeaderColumnPadding;
                strTableRow += @"<TablixCell>
                  <CellContents>
                   " + GenerateTextBox("txtHeader_" + table.ReportName + "_", ColumnCell.Name, columns[i].HeaderText == null || columns[i].HeaderText.Trim() == "" ? ColumnCell.Name : columns[i].HeaderText, false, padding) + @"
                  </CellContents>
                </TablixCell>";
            }
            strTableRow += @"</TablixCells></TablixRow>";
            return strTableRow;
        }

        private static string GetTableColumns(ReportBuilder reportBuilder, ReportBuilder.ReportTable table)
        {
            ReportBuilder.ReportColumns[] columns = table.ReportDataColumns;
            ReportBuilder.ReportTextBoxControl ColumnCell = new ReportBuilder.ReportTextBoxControl();

            if (columns == null) return "";

            string strColumnHeirarchy = "";
            strColumnHeirarchy = @"
            <TablixColumns>";
            for (int i = 0; i < columns.Length; i++)
            {
                ColumnCell = columns[i].ColumnCell;

                strColumnHeirarchy += @" <TablixColumn>
                                          <Width>" + ColumnCell.Size.Width.ToString() + @"cm</Width>
                                        </TablixColumn>";
            }
            strColumnHeirarchy += @"</TablixColumns>";
            return strColumnHeirarchy;
        }

        private static string GetTableColumnHeirarchy(ReportBuilder reportBuilder, ReportBuilder.ReportTable table)
        {
            ReportBuilder.ReportColumns[] columns = table.ReportDataColumns;
            if (columns == null) return "";

            string strColumnHeirarchy = "";
            strColumnHeirarchy = @"
            <TablixColumnHierarchy>
          <TablixMembers>";
            for (int i = 0; i < columns.Length; i++)
            {
                strColumnHeirarchy += "<TablixMember />";
            }
            strColumnHeirarchy += @"</TablixMembers>
        </TablixColumnHierarchy>";
            return strColumnHeirarchy;
        }

        #endregion Report Table Configuration

        #region Report TextBox

        private static string GenerateTextBox(string strControlIDPrefix, string strName, string strValueOrExpression = "", bool isFieldValue = true, ReportBuilder.ReportDimensions padding = null)
        {
            string strTextBox = "";
            strTextBox = @" <Textbox Name=""" + strControlIDPrefix + strName + @""">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>";
            if (isFieldValue) strTextBox += @"<Value>=Fields!" + strName + @".Value</Value>";
            else strTextBox += @"<Value>" + strValueOrExpression + "</Value>";
            strTextBox += @"<Style />
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>" + strControlIDPrefix + strName + @"</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>Solid</Style>
                        </Border>" + GetDimensions(padding) + @"</Style>
                    </Textbox>";
            return strTextBox;
        }

        private static string GetTextBox(string textBoxName, ReportBuilder.ReportDimensions padding = null, params string[] strValues)
        {
            string strTextBox = "";
            strTextBox = @" <Textbox Name=""" + textBoxName + @""">
          <CanGrow>true</CanGrow>
          <KeepTogether>true</KeepTogether>
          <Paragraphs>
            <Paragraph>
              <TextRuns>";

            for (int i = 0; i < strValues.Length; i++)
            {
                strTextBox += GetTextRun(strValues[i].ToString());
            }

            strTextBox += @"</TextRuns>
              <Style />
            </Paragraph>
          </Paragraphs>
          <rd:DefaultName>" + textBoxName + @"</rd:DefaultName>
          <Top>1.0884cm</Top>
          <Left>0cm</Left>
          <Height>0.6cm</Height>
          <Width>7.93812cm</Width>
          <ZIndex>2</ZIndex>
          <Style>
            <Border>
              <Style>None</Style>
            </Border>";

            strTextBox += GetDimensions(padding) + @"</Style>
        </Textbox>";
            return strTextBox;
        }

        private static string GetTextRun(string ValueOrExpression)
        {
            return @"<TextRun>
                  <Value>" + ValueOrExpression + @"</Value>
                  <Style>
                    <FontSize>8pt</FontSize>
                  </Style>
                </TextRun>";
        }

        #endregion Report TextBox

        #region Images

        private static void GenerateReportImage(ReportBuilder reportBuilder)
        {
        }

        #endregion Images

        #region Settings

        private static string GetDimensions(ReportBuilder.ReportDimensions padding = null)
        {
            string strDimensions = "";
            if (padding != null)
            {
                if (padding.Default == 0)
                {
                    strDimensions += string.Format("<PaddingLeft>{0}pt</PaddingLeft>", padding.Left);
                    strDimensions += string.Format("<PaddingRight>{0}pt</PaddingRight>", padding.Right);
                    strDimensions += string.Format("<PaddingTop>{0}pt</PaddingTop>", padding.Top);
                    strDimensions += string.Format("<PaddingBottom>{0}pt</PaddingBottom>", padding.Bottom);
                }
                else
                {
                    strDimensions += string.Format("<PaddingLeft>{0}pt</PaddingLeft>", padding.Default);
                    strDimensions += string.Format("<PaddingRight>{0}pt</PaddingRight>", padding.Default);
                    strDimensions += string.Format("<PaddingTop>{0}pt</PaddingTop>", padding.Default);
                    strDimensions += string.Format("<PaddingBottom>{0}pt</PaddingBottom>", padding.Default);
                }
            }
            return strDimensions;
        }

        #endregion Settings
    }
}