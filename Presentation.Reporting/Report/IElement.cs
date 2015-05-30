namespace Platform.Presentation.Reporting.RDLC
{
    using System.Xml.Linq;

    public interface IElement
    {
        XElement Element { get; }
    }
}
