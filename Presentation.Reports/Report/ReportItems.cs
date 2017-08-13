namespace Platform.Presentation.Reports.RDLC
{
    public class ReportItems : CollectionOf<ReportItem>, IElement
    {
        protected sealed override string GetRdlName()
        {
            return typeof(ReportItems).GetShortName();
        }
    }
}