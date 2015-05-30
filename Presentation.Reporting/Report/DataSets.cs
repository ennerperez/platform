namespace Platform.Presentation.Reporting.RDLC
{
    public class DataSets : CollectionOf<DataSet>, IElement
    {
        protected sealed override string GetRdlName()
        {
            return typeof(DataSets).GetShortName();
        }
    }
}
