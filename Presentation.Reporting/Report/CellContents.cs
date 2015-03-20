﻿namespace Presentation.Reporting.RDLC
{
    public class CellContents : ParentOf<ReportItem>
    {
        public CellContents(ReportItem reportItem)
            : base(reportItem)
        {            
        }

        protected sealed override string GetRdlName()
        {
            return typeof(CellContents).GetShortName();
        }
    }
}
