namespace Platform.Presentation.Reports.RDLC
{
    public class TextRuns : CollectionOf<TextRun>, IElement
    {
        public TextRuns(TextRun textRun)
            : base(textRun)
        {
        }

        protected sealed override string GetRdlName()
        {
            return typeof(TextRuns).GetShortName();
        }
    }
}