namespace Platform.Presentation.Reports.RDLC
{
    public class ReportSections : CollectionOf<ReportSection>, IElement
    {
        protected sealed override string GetRdlName()
        {
            return typeof(ReportSections).GetShortName();
        }
    }
}