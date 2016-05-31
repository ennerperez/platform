namespace Platform.Presentation.Reports.RDLC
{
    public class TopBorder : Border
    {
        protected override string BorderName
        {
            get
            {
                return "Top" + base.BorderName;
            }
        }
    }
}
