using System.Xml.Serialization;

namespace SB.Infrastructure.Entity;

[XmlRoot(ElementName="Registration")]
public class Registration { 

    [XmlElement(ElementName="ServiceID")] 
    public int ServiceID { get; set; } 

    [XmlElement(ElementName="TotalAmount")] 
    public TotalAmount TotalAmount { get; set; } 

    [XmlElement(ElementName="FiscalHostCode")] 
    public string FiscalHostCode { get; set; } 

    [XmlElement(ElementName="FiscalRegisterCode")] 
    public string FiscalRegisterCode { get; set; } 

    [XmlElement(ElementName="ResponseText")] 
    public ResponseText ResponseText { get; set; } 

    [XmlAttribute(AttributeName="TimeStamp")] 
    public DateTime TimeStamp { get; set; } 

    [XmlAttribute(AttributeName="FiscalBatch")] 
    public int FiscalBatch { get; set; } 

    [XmlAttribute(AttributeName="STAN")] 
    public int STAN { get; set; } 

    [XmlText] 
    public string Text { get; set; } 
}