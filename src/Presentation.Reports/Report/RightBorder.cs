namespace Platform.Presentation.Reports.RDLC
{
    public class RightBorder : Border
    {
        protected override string BorderName
        {
            get
            {
                return "Right" + base.BorderName;
            }
        }
    }
}