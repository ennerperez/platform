﻿namespace Platform.Presentation.Reports.RDLC
{
    public class EmbeddedImages : CollectionOf<EmbeddedImage>, IElement
    {
        protected sealed override string GetRdlName()
        {
            return typeof(EmbeddedImages).GetShortName();
        }
    }
}