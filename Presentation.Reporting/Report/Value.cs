namespace Platform.Presentation.Reporting.RDLC
{
    public class Value : ParentOf<string>
    {
        public Value(string item)
            : base(item)
        {
        }

        protected sealed override string GetRdlName()
        {
            return typeof(Value).GetShortName();
        }
    }
}
