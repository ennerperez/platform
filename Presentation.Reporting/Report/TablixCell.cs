namespace Platform.Presentation.Reporting.RDLC
{
    public class TablixCell : ParentOf<CellContents>
    {
        public TablixCell(CellContents item)
            : base(item)
        {
        }

        protected sealed override string GetRdlName()
        {
            return typeof(TablixCell).GetShortName();
        }
    }
}
