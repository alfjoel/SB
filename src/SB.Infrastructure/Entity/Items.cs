using System.Xml.Serialization;

namespace SB.Infrastructure.Entity;


[XmlRoot(ElementName="Items")]
public class Items { 

    [XmlElement(ElementName="ItemInfo")] 
    public List<ItemInfo> ItemInfo { get; set; } 
}