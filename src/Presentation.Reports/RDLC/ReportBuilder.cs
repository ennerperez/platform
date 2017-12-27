using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Platform.Presentation.Reports.RDLC
{
    public class ReportBuilder : ReportBuilder<dynamic>
    {
    }

    public class ReportBuilder<T> : IReportBuilder<T>
    {
        public string Logo { get; set; }

        public ReportPage Page { get; set; }
        public ReportBody Body { get; set; }
        public System.Data.DataSet DataSource { get; set; }

        private bool autoGenerateReport = true;

        public bool AutoGenerateReport
        {
            get { return autoGenerateReport; }
            set { autoGenerateReport = value; }
        }

        public string BuildReport(T model = default(T))
        {
            throw new System.NotImplementedException();
        }

        public static class ReportGlobalParameters
        {
            public static string CurrentPageNumber = "=Globals!PageNumber";
            public static string TotalPages = "=Globals!OverallTotalPages";
        }

        public class ReportItems
        {
            public ReportTextBoxControl[] TextBoxControls { get; set; }
            public ReportTable[] ReportTable { get; set; }
            public ReportImage[] ReportImages { get; set; }
        }

        public class ReportTable
        {
            public string ReportName { get; set; }
            public ReportColumns[] ReportDataColumns { get; set; }
        }

        public class ReportColumns
        {
            public bool isGroupedColumn { get; set; }
            public string HeaderText { get; set; }
            public ReportSort SortDirection { get; set; }
            public ReportFunctions Aggregate { get; set; }
            public ReportTextBoxControl ColumnCell { get; set; }
            public ReportDimensions HeaderColumnPadding { get; set; }
        }

        public class ReportTextBoxControl
        {
            public string Name { get; set; }
            public string[] ValueOrExpression { get; set; }
            public ReportActions Action { get; set; }
            public ReportDimensions Padding { get; set; }
            public int SpaceAfter { get; set; }
            public int SpaceBefore { get; set; }

            private ReportHorizantalAlign textAlign = ReportHorizantalAlign.Default;

            public ReportHorizantalAlign TextAlign
            {
                get { return textAlign; }
                set { textAlign = value; }
            }

            private ReportHorizantalAlign verticalAlign = ReportHorizantalAlign.Default;

            public ReportHorizantalAlign VerticalAlign
            {
                get { return verticalAlign; }
                set { verticalAlign = value; }
            }

            public ReportStyles BorderStyle { get; set; }
            public ReportColor BorderColor { get; set; }
            public ReportScale BorderWidth { get; set; }
            public Color BackgroundColor { get; set; }
            public ReportImage BackgroundImage { get; set; }
            public Font TextFont { get; set; }
            public double LineHeight { get; set; }
            public bool CanGrow { get; set; }
            public bool CanShrink { get; set; }
            public bool ToolTip { get; set; }
            public ReportDimensions Position { get; set; }
            public ReportScale Size { get; set; }
            public bool Visible { get; set; }
        }

        public class ReportBody
        {
            public ReportSections ReportBodySection { get; set; }
            public ReportItems ReportControlItems { get; set; }
        }

        public class ReportPage
        {
            public bool AutoRefresh { get; set; }
            public Color BackgroundColor { get; set; }
            public ReportImage BackgroundImage { get; set; }
            public ReportColor BorderColor { get; set; }
            public ReportScale BorderWidth { get; set; }
            public ReportColumnSettings Columns { get; set; }
            public ReportScale InteractiveSize { get; set; }
            public ReportDimensions Margins { get; set; }
            public ReportScale PageSize { get; set; }
            public ReportSections ReportHeader { get; set; }
            public ReportSections ReportFooter { get; set; }
        }

        public class ReportSections
        {
            public ReportStyles BorderStyle { get; set; }
            public ReportColor BorderColor { get; set; }
            public ReportScale BorderWidth { get; set; }
            public Color BackgroundColor { get; set; }
            public ReportImage BackgroundImage { get; set; }
            public ReportScale Size { get; set; }

            private bool printOnFirstPage = true;

            public bool PrintOnFirstPage
            {
                get { return printOnFirstPage; }
                set { printOnFirstPage = value; }
            }

            private bool printOnLastpage = true;

            public bool PrintOnLastPage
            {
                get { return printOnLastpage; }
                set { printOnLastpage = value; }
            }

            public ReportItems ReportControlItems { get; set; }
        }

        public class ReportColumnSettings
        {
            public int Columns { get; set; }
            public int ColumnsSpacing { get; set; }
        }

        public class ReportActions
        {
            public ReportActionType ActionType { get; set; }
            public string ValueOrExpression { get; set; }
        }

        public class ReportDimensions
        {
            public double Left { get; set; }
            public double Right { get; set; }
            public double Top { get; set; }
            public double Bottom { get; set; }
            private double _default = 2;

            public double Default
            {
                get { return _default; }
                set { _default = value; }
            }
        }

        public class ReportIndent
        {
            public double HangingIndent { get; set; }
            public double LeftIndent { get; set; }
            public double RightIndent { get; set; }
        }

        public class ReportScale
        {
            public double Height { get; set; }
            public double Width { get; set; }
        }

        public class ReportImage
        {
            public ReportImageSource ImagePath { get; set; }
            public string ValueOrExpression { get; set; }
            public ReportImageMIMEType MIMEType { get; set; }
            public ReportStyles Border { get; set; }
            public ReportColor Color { get; set; }
            public ReportDimensions Position { get; set; }
            public ReportScale Size { get; set; }
            public ReportDimensions Padding { get; set; }

            private ReportImageScaling reportImageScaling = ReportImageScaling.AutoSize;

            public ReportImageScaling ReportImageScaling
            {
                get { return reportImageScaling; }
                set { reportImageScaling = value; }
            }
        }

        public class ReportColor
        {
            public Color Default { get; set; }
            public Color Left { get; set; }
            public Color Right { get; set; }
            public Color Top { get; set; }
            public Color Bottom { get; set; }
        }

        public class ReportStyles
        {
            public ReportStyle Default { get; set; }
            public ReportStyle Left { get; set; }
            public ReportStyle Right { get; set; }
            public ReportStyle Top { get; set; }
            public ReportStyle Bottom { get; set; }
        }

        public enum ReportActionType
        {
            None,
            HyperLink
        }

        public enum ReportHorizantalAlign
        {
            Left,
            Right,
            Center,
            General,
            Default
        }

        public enum ReportVerticalAlign
        {
            Top,
            Middle,
            Bottom,
            Default
        }

        public enum ReportImageRepeat
        {
            Default,
            Repeat,
            RepeatX,
            RepeatY,
            Clip
        }

        public enum ReportImageScaling
        {
            AutoSize,
            Flip,
            FlipProportional,
            Clip
        }

        public enum ReportImageSource
        {
            External,
            Embedded,
            Database
        }

        public enum ReportImageMIMEType
        {
            Bitmap,
            JPEG,
            GIF,
            PNG,
            xPNG
        }

        public enum ReportStyle
        {
            Default, Dashed, Dotted, Double, Solid, None
        }

        public enum ReportSort
        {
            Ascending,
            Descending
        }

        public enum ReportFunctions
        {
            Avg,
            Count,
            Sum,
            Min,
            Max,
            Aggregate
        }
    }
}