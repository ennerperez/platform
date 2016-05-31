﻿namespace Platform.Presentation.Reports.RDLC
{
    public class TablixCorner : ParentOf<TablixCornerRows>
    {
        public TablixCorner(TablixCornerRows item)
            : base(item)
        {
        }

        protected sealed override string GetRdlName()
        {
            return typeof(TablixCorner).GetShortName();
        }
    }
}