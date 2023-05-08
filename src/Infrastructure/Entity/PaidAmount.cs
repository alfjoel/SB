using System.Xml.Serialization;

namespace Infrastructure.Entity;

[XmlRoot(ElementName="PaidAmount")]
public class PaidAmount { 

    [XmlAttribute(AttributeName="Type")] 
    public string Type { get; set; } 

    [XmlAttribute(AttributeName="Currency")] 
    public string Currency { get; set; } 

    [XmlText] 
    public double Text { get; set; } 
}