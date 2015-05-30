namespace Platform.Presentation.Reporting.RDLC
{
    public class TablixCells : CollectionOf<TablixCell>, IElement
    {
        public TablixCells(TablixCell tablixCell)
            : base(tablixCell)
        {
        }

        protected sealed override string GetRdlName()
        {
            return typeof(TablixCells).GetShortName();
        }
    }
}
