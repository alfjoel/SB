using System.Xml.Serialization;

namespace SB.Infrastructure.Entity;

[XmlRoot(ElementName = "TaxRate")]
public class TaxRate
{
    [XmlAttribute(AttributeName = "TaxType")]
    public string TaxType { get; set; }

    [XmlText] public double Text { get; set; }
}