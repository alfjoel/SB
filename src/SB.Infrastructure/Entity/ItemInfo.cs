using System.Xml.Serialization;

namespace SB.Infrastructure.Entity;

[XmlRoot(ElementName="ItemInfo")]
public class ItemInfo { 

    [XmlElement(ElementName="ArticleId")] 
    public int ArticleId { get; set; } 

    [XmlElement(ElementName="Name")] 
    public string Name { get; set; } 

    [XmlElement(ElementName="EPAN")] 
    public string Epan { get; set; } 
    
    [XmlIgnore]
    public decimal? Quantity { get; set; }

    [XmlElement("Quantity")]
    public string QuantityString
    {
        get { return Quantity.HasValue ? Quantity.ToString() : ""; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                Quantity = null;
            }
            else
            {
                Quantity = decimal.Parse(value);
            }
        }
    }

    [XmlElement(ElementName="Amount")] 
    public double Amount { get; set; } 

    [XmlElement(ElementName="TaxRate")] 
    public TaxRate TaxRate { get; set; } 

    [XmlElement(ElementName="EntryTime")] 
    public DateTime EntryTime { get; set; } 
}