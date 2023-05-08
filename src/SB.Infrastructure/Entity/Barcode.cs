using System.Xml.Serialization;

namespace SB.Infrastructure.Entity;


[XmlRoot(ElementName="Barcode")]
public class Barcode { 

    [XmlAttribute(AttributeName="Type")] 
    public string Type { get; set; } 

    [XmlAttribute(AttributeName="ModuleWidth")] 
    public int ModuleWidth { get; set; } 

    [XmlAttribute(AttributeName="Module-Height")] 
    public int ModuleHeight { get; set; } 

    [XmlText] 
    public string Text { get; set; } 
}