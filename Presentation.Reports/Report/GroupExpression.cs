namespace Platform.Presentation.Reports.RDLC
{
    public class GroupExpression : ParentOf<string>
    {
        public GroupExpression(string item)
            : base(item)
        {
        }

        protected sealed override string GetRdlName()
        {
            return typeof(GroupExpression).GetShortName();
        }
    }
}
