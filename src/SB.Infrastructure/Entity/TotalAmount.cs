using System.Xml.Serialization;

namespace SB.Infrastructure.Entity;

[XmlRoot(ElementName = "TotalAmount")]
public class TotalAmount
{
    [XmlAttribute(AttributeName = "Currency")]
    public string Currency { get; set; }

    [XmlText] public decimal Text { get; set; }
}