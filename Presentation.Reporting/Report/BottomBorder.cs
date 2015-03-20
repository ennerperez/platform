namespace Presentation.Reporting.RDLC
{
    public class BottomBorder : Border
    {
        protected override string BorderName
        {
            get
            {
                return "Bottom" + base.BorderName;
            }
        }
    }
}
