﻿namespace Presentation.Reporting.RDLC
{
    public class TablixCornerRows : CollectionOf<TablixCornerRow>, IElement
    {
        public TablixCornerRows(TablixCornerRow tablixCornerRow)
            : base(tablixCornerRow)
        {
        }

        protected sealed override string GetRdlName()
        {
            return typeof(TablixCornerRows).GetShortName();
        }
    }
}
