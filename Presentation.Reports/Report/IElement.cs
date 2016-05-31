namespace Platform.Presentation.Reports.RDLC
{
    using System.Xml.Linq;

    public interface IElement
    {
        XElement Element { get; }
    }
}
