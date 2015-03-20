namespace Presentation.Reporting.RDLC
{
    public class TablixCornerCell : ParentOf<CellContents>
    {
        public TablixCornerCell(CellContents item)
            : base(item)
        {
        }

        protected sealed override string GetRdlName()
        {
            return typeof(TablixCornerCell).GetShortName();
        }
    }
}
