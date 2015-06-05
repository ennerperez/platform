﻿namespace Platform.Presentation.Reporting.RDLC
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