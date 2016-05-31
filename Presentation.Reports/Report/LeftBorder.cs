namespace Platform.Presentation.Reports.RDLC
{
    public class LeftBorder : Border
    {
        protected override string BorderName
        {
            get
            {
                return "Left" + base.BorderName;
            }
        }
    }
}
