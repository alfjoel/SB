using System.Xml.Serialization;

namespace SB.Infrastructure.Entity;

[XmlRoot(ElementName="ResponseText")]
public class ResponseText { 

    [XmlAttribute(AttributeName="LanguageCode")] 
    public string LanguageCode { get; set; } 

    [XmlText] 
    public string Text { get; set; } 
}