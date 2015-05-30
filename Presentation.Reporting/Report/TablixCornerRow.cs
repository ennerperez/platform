namespace Platform.Presentation.Reporting.RDLC
{
    public class TablixCornerRow : ParentOf<TablixCornerCell>
    {
        public TablixCornerRow(TablixCornerCell item)
            : base(item)
        {
        }

        protected sealed override string GetRdlName()
        {
            return typeof(TablixCornerRow).GetShortName();
        }
    }
}
