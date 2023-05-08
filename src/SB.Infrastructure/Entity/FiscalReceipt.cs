using System.Xml.Serialization;

namespace SB.Infrastructure.Entity;

[XmlRoot(ElementName="FiscalReceipt")]
public class FiscalReceipt { 

    [XmlElement(ElementName="PrintData")] 
    public string PrintData { get; set; } 

    [XmlElement(ElementName="Barcode")] 
    public Barcode Barcode { get; set; } 

    [XmlAttribute(AttributeName="LanguageCode")] 
    public string LanguageCode { get; set; } 

    [XmlText] 
    public string Text { get; set; } 
}