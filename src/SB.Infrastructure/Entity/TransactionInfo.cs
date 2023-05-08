using System.Xml.Serialization;

namespace SB.Infrastructure.Entity;

[XmlRoot(ElementName="TransactionInfo")]
public class TransactionInfo { 

    [XmlElement(ElementName="TimeStamp")] 
    public DateTime TimeStamp { get; set; } 

    [XmlElement(ElementName="TotalAmount")] 
    public TotalAmount TotalAmount { get; set; } 

    [XmlElement(ElementName="PaidAmount")] 
    public List<PaidAmount> PaidAmount { get; set; } 
}