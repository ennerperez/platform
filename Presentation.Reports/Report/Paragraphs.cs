namespace Platform.Presentation.Reports.RDLC
{
    public class Paragraphs : CollectionOf<Paragraph>, IElement
    {
        public Paragraphs()
        {            
        }

        public Paragraphs(Paragraph paragraph)
            : base(paragraph)
        {
        }

        protected sealed override string GetRdlName()
        {
            return typeof(Paragraphs).GetShortName();
        }
    }
}
