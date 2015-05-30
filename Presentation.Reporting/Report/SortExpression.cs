﻿namespace Platform.Presentation.Reporting.RDLC
{
    public class SortExpression : ParentOf<Value>
    {
        public SortExpression(Value value)
            : base(value)
        {
        }

        protected sealed override string GetRdlName()
        {
            return typeof(SortExpression).GetShortName();
        }
    }
}
