using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Platform.Presentation.Reports.RDLC
{
    internal static class ReportEngine<T>
    {
        #region Initialize

        public static Stream GenerateReport(ReportBuilder<T> reportBuilder)
        {
            Stream ret = new MemoryStream(Encoding.UTF8.GetBytes(GetReportData(reportBuilder)));
            return ret;
        }

        private static ReportBuilder<T> InitAutoGenerateReport(ReportBuilder<T> reportBuilder)
        {
            if (reportBuilder != null && reportBuilder.DataSource != null && reportBuilder.DataSource.Tables.Count > 0)
            {
                System.Data.DataSet ds = reportBuilder.DataSource;

                int _TablesCount = ds.Tables.Count;
                ReportBuilder<T>.ReportTable[] reportTables = new ReportBuilder<T>.ReportTable[_TablesCount];

                if (reportBuilder.AutoGenerateReport)
                {
                    for (int j = 0; j < _TablesCount; j++)
                    {
                        System.Data.DataTable dt = ds.Tables[j];
                        ReportBuilder<T>.ReportColumns[] columns = new ReportBuilder<T>.ReportColumns[dt.Columns.Count];
                        ReportBuilder<T>.ReportScale ColumnScale = new ReportBuilder<T>.ReportScale();
                        ColumnScale.Width = 4;
                        ColumnScale.Height = 1;
                        ReportBuilder<T>.ReportDimensions ColumnPadding = new ReportBuilder<T>.ReportDimensions();
                        ColumnPadding.Default = 2;
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            columns[i] = new ReportBuilder<T>.ReportColumns() { ColumnCell = new ReportBuilder<T>.ReportTextBoxControl() { Name = dt.Columns[i].ColumnName, Size = ColumnScale, Padding = ColumnPadding }, HeaderText = dt.Columns[i].ColumnName, HeaderColumnPadding = ColumnPadding };
                        }

                        reportTables[j] = new ReportBuilder<T>.ReportTable() { ReportName = dt.TableName, ReportDataColumns = columns };
                    }
                }
                reportBuilder.Body = new ReportBuilder<T>.ReportBody
                {
                    ReportControlItems = new ReportBuilder<T>.ReportItems()
                };
                reportBuilder.Body.ReportControlItems.ReportTable = reportTables;
            }
            return reportBuilder;
        }

        public static string GetReportData(ReportBuilder<T> reportBuilder)
        {
            reportBuilder = InitAutoGenerateReport(reportBuilder);

            var logo = string.Empty;
            if (string.IsNullOrEmpty(reportBuilder.Logo) && System.IO.File.Exists(reportBuilder.Logo))
            {
                byte[] imgBinary = File.ReadAllBytes(reportBuilder.Logo);
                if (imgBinary.Length > 0)
                    logo = Convert.ToBase64String(imgBinary);
            }

            return $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <Report xmlns=""http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition""
                    xmlns:rd=""http://schemas.microsoft.com/SQLServer/reporting/reportdesigner"">
                {GenerateBody(reportBuilder)}
                <Width>20.8cm</Width>
                <Page>
                    {GetPageHeader(reportBuilder)}
                    {GetFooter(reportBuilder)}
                    {GetReportPageSettings()}
                    <Style />
                </Page>
                <AutoRefresh>0</AutoRefresh>
                {GetDataSet(reportBuilder)}
                <EmbeddedImages>
                    <EmbeddedImage Name=""Logo"">
                        <MIMEType>image/png</MIMEType>
                            <ImageData>{logo}</ImageData>
                    </EmbeddedImage>
                </EmbeddedImages>
                <Language>{System.Globalization.CultureInfo.CurrentCulture.Name}</Language>
                <ConsumeContainerWhitespace>true</ConsumeContainerWhitespace>
                <rd:ReportUnitType>Cm</rd:ReportUnitType>
                <rd:ReportID>{Guid.NewGuid().ToString()}</rd:ReportID>
            </Report>";
        }

        #endregion Initialize

        private static string GenerateBody(ReportBuilder<T> reportBuilder)
        {
            var _tableData = GenerateTable(reportBuilder);

            return $@"<Body>
                {(!string.IsNullOrEmpty(_tableData) ? $@"<ReportItems>{_tableData}</ReportItems>" : "")}
                <Height>2.1162cm</Height>
                <Style />
            </Body>";
        }

        #region Page Settings

        private static string GetReportPageSettings()
        {
            return $@"<PageHeight>21cm</PageHeight>
            <PageWidth>29.5cm</PageWidth>
            <LeftMargin>0.1cm</LeftMargin>
            <RightMargin>0.1cm</RightMargin>
            <TopMargin>0.1cm</TopMargin>
            <BottomMargin>0.1cm</BottomMargin>
            <ColumnSpacing>1cm</ColumnSpacing>";
        }

        private static string GetPageHeader(ReportBuilder<T> reportBuilder)
        {
            if (reportBuilder.Page == null || reportBuilder.Page.ReportHeader == null) return "";
            var reportHeader = reportBuilder.Page.ReportHeader;
            var headerTxt = reportBuilder.Page.ReportHeader.ReportControlItems.TextBoxControls;
            var reportItems = new StringBuilder();
            if (headerTxt != null)
                for (int i = 0; i < headerTxt.Count(); i++)
                    reportItems.Append(GetTextBox(headerTxt[i].Name, null, headerTxt[i].ValueOrExpression));

            return $@"<PageHeader>
                <Height>{reportHeader.Size.Height.ToString()}in</Height>
                <PrintOnFirstPage>{reportHeader.PrintOnFirstPage.ToString().ToLower()}</PrintOnFirstPage>
                <PrintOnLastPage>{reportHeader.PrintOnLastPage.ToString().ToLower()}</PrintOnLastPage>
                <ReportItems>
                    {reportItems.ToString()}
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
        }

        private static string GetFooter(ReportBuilder<T> reportBuilder)
        {
            if (reportBuilder.Page == null || reportBuilder.Page.ReportFooter == null) return "";
            var footerTxt = reportBuilder.Page.ReportFooter.ReportControlItems.TextBoxControls;
            var reportItems = new StringBuilder();
            if (footerTxt != null)
                for (int i = 0; i < footerTxt.Count(); i++)
                    reportItems.Append(GetTextBox(footerTxt[i].Name, null, footerTxt[i].ValueOrExpression));

            return $@"<PageFooter>
                <Height>0.68425in</Height>
                <PrintOnFirstPage>true</PrintOnFirstPage>
                <PrintOnLastPage>true</PrintOnLastPage>
                <ReportItems>
                    {reportItems.ToString()}
                </ReportItems>
                <Style />
            </PageFooter>";
        }

        #endregion Page Settings

        #region Dataset

        private static string GetDataSet(ReportBuilder<T> reportBuilder)
        {
            var dataSetStr = new StringBuilder();
            if (reportBuilder != null && reportBuilder.DataSource != null && reportBuilder.DataSource.Tables.Count > 0)
            {
                string dsName = $"rpt{typeof(T).ToString().Replace(".", "_")}";
                dataSetStr.Append($@"
                <DataSources>
                    <DataSource Name=""{dsName}"">
                        <ConnectionProperties>
                            <DataProvider>System.Data.DataSet</DataProvider>
                            <ConnectString>/* Local Connection */</ConnectString>
                        </ConnectionProperties>
                        <rd:DataSourceID>{Guid.NewGuid().ToString()}</rd:DataSourceID>
                    </DataSource>
                </DataSources>
                {GetDataSetTables(reportBuilder.Body.ReportControlItems.ReportTable, dsName)}");
            }
            return dataSetStr.ToString();
        }

        private static string GetDataSetTables(ReportBuilder<T>.ReportTable[] tables, string DataSourceName)
        {
            var strTables = new StringBuilder();

            for (int i = 0; i < tables.Length; i++)
            {
                strTables.Append($@"<DataSet Name=""{tables[i].ReportName}"">
                    <Query>
                        <DataSourceName>{DataSourceName}</DataSourceName>
                        <CommandText>/* Local Query */</CommandText>
                    </Query>
                    {GetDataSetFields(tables[i].ReportDataColumns)}
                </DataSet>");
            }

            return $@"<DataSets>
                {strTables.ToString()}
            </DataSets>";
        }

        private static string GetDataSetFields(ReportBuilder<T>.ReportColumns[] reportColumns)
        {
            var strFields = new StringBuilder();

            for (int i = 0; i < reportColumns.Length; i++)
            {
                strFields.Append($@"<Field Name=""{reportColumns[i].ColumnCell.Name}"">
                    <DataField>{reportColumns[i].ColumnCell.Name}</DataField>
                    <rd:TypeName>System.String</rd:TypeName>
                </Field>");
            }

            return $@"<Fields>
                {strFields.ToString()}
            </Fields>";
        }

        #endregion Dataset

        #region Report Table Configuration

        private static string GenerateTable(ReportBuilder<T> reportBuilder)
        {
            var TableStr = new StringBuilder();
            if (reportBuilder != null && reportBuilder.DataSource != null && reportBuilder.DataSource.Tables.Count > 0)
            {
                var table = new ReportBuilder<T>.ReportTable();
                for (int i = 0; i < reportBuilder.Body.ReportControlItems.ReportTable.Length; i++)
                {
                    table = reportBuilder.Body.ReportControlItems.ReportTable[i];
                    TableStr.Append($@"<Tablix Name=""table_{table.ReportName}"">
                        <TablixBody>
                            {GetTableColumns(reportBuilder, table)}
                            <TablixRows>
                                {GenerateTableHeaderRow(reportBuilder, table) + GenerateTableRow(reportBuilder, table)}
                            </TablixRows>
                        </TablixBody>{GetTableColumnHeirarchy(reportBuilder, table)}
                        <TablixRowHierarchy>
                            <TablixMembers>
                                <TablixMember>
                                    <KeepWithGroup>After</KeepWithGroup>
                                </TablixMember>
                                <TablixMember>
                                    <Group Name=""{table.ReportName + "_Details"}"" />
                                </TablixMember>
                            </TablixMembers>
                        </TablixRowHierarchy>
                        <RepeatColumnHeaders>true</RepeatColumnHeaders>
                        <RepeatRowHeaders>true</RepeatRowHeaders>
                        <DataSetName>{table.ReportName}</DataSetName>
                        {GetSortingDetails(reportBuilder)}
                        <Top>0.07056cm</Top>
                        <Left>0cm</Left>
                        <Height>1.2cm</Height>
                        <Width>7.5cm</Width>
                        <Style>
                            <Border>
                                <Style>None</Style>
                            </Border>
                        </Style>
                    </Tablix>");
                }
            }
            return TableStr.ToString();
        }

        private static string GetSortingDetails(ReportBuilder<T> reportBuilder)
        {
            var tables = reportBuilder.Body.ReportControlItems.ReportTable;
            var columns = reportBuilder.Body.ReportControlItems.ReportTable[0].ReportDataColumns;
            var sortColumn = new ReportBuilder<T>.ReportTextBoxControl();
            if (columns == null) return "";

            var strSorting = new StringBuilder();

            for (int i = 0; i < columns.Length; i++)
            {
                sortColumn = columns[i].ColumnCell;
                strSorting.Append($@"<SortExpression>
                    <Value>=Fields!{sortColumn.Name}.Value</Value>
                    {(columns[i].SortDirection == ReportBuilder<T>.ReportSort.Descending ? @"<Direction>Descending</Direction>" : "")}
                </SortExpression>");
            }

            return $@"<SortExpressions>
                {strSorting}
            </SortExpressions>";
        }

        private static string GenerateTableRow(ReportBuilder<T> reportBuilder, ReportBuilder<T>.ReportTable table)
        {
            var columns = table.ReportDataColumns;
            var ColumnCell = new ReportBuilder<T>.ReportTextBoxControl();
            var colHeight = ColumnCell.Size;
            var padding = new ReportBuilder<T>.ReportDimensions();
            if (columns == null) return "";

            var strTableRow = new StringBuilder();
            for (int i = 0; i < columns.Length; i++)
            {
                ColumnCell = columns[i].ColumnCell;
                padding = ColumnCell.Padding;
                strTableRow.Append($@"<TablixCell>
                  <CellContents>
                   {GenerateTextBox($"txtCell_{table.ReportName}_", ColumnCell.Name, "", true, padding)}
                  </CellContents>
                </TablixCell>");
            }

            return $@"<TablixRow>
                <Height>0.6cm</Height>
                <TablixCells>
                    {strTableRow.ToString()}
                </TablixCells>
            </TablixRow>";
        }

        private static string GenerateTableHeaderRow(ReportBuilder<T> reportBuilder, ReportBuilder<T>.ReportTable table)
        {
            var columns = table.ReportDataColumns;
            var ColumnCell = new ReportBuilder<T>.ReportTextBoxControl();
            var padding = new ReportBuilder<T>.ReportDimensions();
            if (columns == null) return "";

            var strTableRow = new StringBuilder();

            for (int i = 0; i < columns.Length; i++)
            {
                ColumnCell = columns[i].ColumnCell;
                padding = columns[i].HeaderColumnPadding;
                strTableRow.Append($@"<TablixCell>
                  <CellContents>
                   {GenerateTextBox($"txtHeader_{table.ReportName}_", ColumnCell.Name, columns[i].HeaderText == null || columns[i].HeaderText.Trim() == "" ? ColumnCell.Name : columns[i].HeaderText, false, padding)}
                  </CellContents>
                </TablixCell>");
            }
            return $@"<TablixRow>
                <Height>0.6cm</Height>
                <TablixCells>
                    {strTableRow.ToString()}
                </TablixCells>
            </TablixRow>";
        }

        private static string GetTableColumns(ReportBuilder<T> reportBuilder, ReportBuilder<T>.ReportTable table)
        {
            var columns = table.ReportDataColumns;
            var ColumnCell = new ReportBuilder<T>.ReportTextBoxControl();

            if (columns == null) return "";

            var strColumnHeirarchy = new StringBuilder();
            for (int i = 0; i < columns.Length; i++)
            {
                ColumnCell = columns[i].ColumnCell;
                strColumnHeirarchy.Append($@"<TablixColumn>
                    <Width>{ColumnCell.Size.Width.ToString()}cm</Width>
                </TablixColumn>");
            }

            return $@"<TablixColumns>
                {strColumnHeirarchy}
            </TablixColumns>";
        }

        private static string GetTableColumnHeirarchy(ReportBuilder<T> reportBuilder, ReportBuilder<T>.ReportTable table)
        {
            var columns = table.ReportDataColumns;
            if (columns == null) return "";

            var strColumnHeirarchy = new StringBuilder();

            for (int i = 0; i < columns.Length; i++)
                strColumnHeirarchy.Append(@"<TablixMember />");

            return $@"<TablixColumnHierarchy>
                <TablixMembers>
                    {strColumnHeirarchy.ToString()}
                </TablixMembers>
            </TablixColumnHierarchy>";
        }

        #endregion Report Table Configuration

        #region Report TextBox

        private static string GenerateTextBox(string strControlIDPrefix, string strName, string strValueOrExpression = "", bool isFieldValue = true, ReportBuilder<T>.ReportDimensions padding = null)
        {
            return $@"<Textbox Name=""{strControlIDPrefix + strName}"">
                <CanGrow>true</CanGrow>
                <KeepTogether>true</KeepTogether>
                <Paragraphs>
                    <Paragraph>
                        <TextRuns>
                            <TextRun>
                                <Value>{(isFieldValue ? $@"=Fields!{strName}.Value" : $@"{strValueOrExpression}")}</Value>
                                <Style />
                            </TextRun>
                        </TextRuns>
                        <Style />
                    </Paragraph>
                </Paragraphs>
                <rd:DefaultName>{strControlIDPrefix + strName}</rd:DefaultName>
                <Style>
                    <Border>
                        <Color>LightGrey</Color>
                        <Style>Solid</Style>
                    </Border>
                    {GetDimensions(padding)}
                </Style>
            </Textbox>";
        }

        private static string GetTextBox(string textBoxName, ReportBuilder<T>.ReportDimensions padding = null, params string[] strValues)
        {
            var textRun = new StringBuilder();
            for (int i = 0; i < strValues.Length; i++)
                textRun.Append(GetTextRun(strValues[i].ToString()));

            return $@"<Textbox Name=""{textBoxName}"">
                <CanGrow>true</CanGrow>
                <KeepTogether>true</KeepTogether>
                <Paragraphs>
                    <Paragraph>
                        <TextRuns>
                            {textRun.ToString()}
                        </TextRuns>
                        <Style />
                    </Paragraph>
                </Paragraphs>
                <rd:DefaultName>{textBoxName}</rd:DefaultName>
                <Top>1.0884cm</Top>
                <Left>0cm</Left>
                <Height>0.6cm</Height>
                <Width>7.93812cm</Width>
                <ZIndex>2</ZIndex>
                <Style>
                    <Border>
                        <Style>None</Style>
                    </Border>
                    {GetDimensions(padding)}
                </Style>
            </Textbox>";
        }

        private static string GetTextRun(string ValueOrExpression)
        {
            return $@"<TextRun>
                <Value>{ValueOrExpression}</Value>
                <Style>
                    <FontSize>8pt</FontSize>
                </Style>
            </TextRun>";
        }

        #endregion Report TextBox

        #region Images

        private static void GenerateReportImage(ReportBuilder<T> reportBuilder)
        {
        }

        #endregion Images

        #region Settings

        private static string GetDimensions(ReportBuilder<T>.ReportDimensions padding = null)
        {
            var strDimensions = new StringBuilder();
            if (padding != null)
            {
                if (padding.Default == 0)
                {
                    strDimensions.Append($@"<PaddingLeft>{padding.Left}pt</PaddingLeft>");
                    strDimensions.Append($@"<PaddingRight>{padding.Right}pt</PaddingRight>");
                    strDimensions.Append($@"<PaddingTop>{padding.Top}pt</PaddingTop>");
                    strDimensions.Append($@"<PaddingBottom>{padding.Bottom}pt</PaddingBottom>");
                }
                else
                {
                    strDimensions.Append($@"<PaddingLeft>{padding.Default}pt</PaddingLeft> ");
                    strDimensions.Append($@"<PaddingRight>{padding.Default}pt</PaddingRight>");
                    strDimensions.Append($@"<PaddingTop>{padding.Default}pt</PaddingTop>");
                    strDimensions.Append($@"<PaddingBottom>{padding.Default}pt</PaddingBottom>");
                }
            }
            return strDimensions.ToString();
        }

        #endregion Settings
    }
}