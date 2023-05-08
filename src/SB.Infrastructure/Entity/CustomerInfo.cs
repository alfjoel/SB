using System.Xml.Serialization;

namespace SB.Infrastructure.Entity;

[XmlRoot(ElementName="CustomerInfo")]
public class CustomerInfo { 

    [XmlElement(ElementName="TaxId")] 
    public int TaxId { get; set; } 

    [XmlElement(ElementName="CustomerName")] 
    public string CustomerName { get; set; } 

    [XmlElement(ElementName="CompanyName")] 
    public string CompanyName { get; set; } 

    [XmlElement(ElementName="Address")] 
    public string Address { get; set; } 

    [XmlElement(ElementName="Country")] 
    public string Country { get; set; } 

    [XmlElement(ElementName="PostCode")] 
    public int PostCode { get; set; } 

    [XmlElement(ElementName="City")] 
    public string City { get; set; } 

    [XmlElement(ElementName="Email")] 
    public string Email { get; set; } 

    [XmlElement(ElementName="Phone")] 
    public double Phone { get; set; } 

    [XmlElement(ElementName="LPN")] 
    public string LPN { get; set; } 

    [XmlAttribute(AttributeName="LanguageCode")] 
    public string LanguageCode { get; set; } 

    [XmlText] 
    public string Text { get; set; } 
}
