using System.Xml.Serialization;

namespace Infrastructure.Entity;

public class ItemInfo
{
    [XmlElement(ElementName="ArticleId")] 
    public int ArticleId { get; set; } 

    [XmlElement(ElementName="Name")] 
    public string Name { get; set; } 

    [XmlElement(ElementName="Epan")] 
    public double Epan { get; set; } 

    [XmlElement(ElementName="Quantity")] 
    public int Quantity { get; set; } 

    [XmlElement(ElementName="Amount")] 
    public double Amount { get; set; } 

    [XmlElement(ElementName="TaxRate")] 
    public TaxRate TaxRate { get; set; } 

    [XmlElement(ElementName="EntryTime")] 
    public DateTime EntryTime { get; set; }
}