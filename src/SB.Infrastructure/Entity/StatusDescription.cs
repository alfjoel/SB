using System.Xml.Serialization;

namespace SB.Infrastructure.Entity;

[XmlRoot(ElementName="StatusDescription")]
public class StatusDescription { 

    [XmlAttribute(AttributeName="LanguageCode")] 
    public string LanguageCode { get; set; } 

    [XmlText] 
    public string Text { get; set; } 
}