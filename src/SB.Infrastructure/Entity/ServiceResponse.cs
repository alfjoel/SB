using System.Xml.Serialization;
using SB.Infrastructure.Interfaces;

namespace SB.Infrastructure.Entity;

public class ServiceResponse : IEmv
{
    [XmlElement(ElementName = "MerchantID")]
    public string MerchantId { get; set; }

    [XmlElement(ElementName = "ZRNumber")] public int ZrNumber { get; set; }

    [XmlElement(ElementName = "DeviceNumber")]
    public int DeviceNumber { get; set; }

    [XmlElement(ElementName = "DeviceType")]
    public int DeviceType { get; set; }

    [XmlElement(ElementName = "FRSMerchantID")]
    public int FrsMerchantId { get; set; }

    [XmlElement(ElementName = "FRSName")] public string FrsName { get; set; }

    [XmlElement(ElementName = "FRSVersion")]
    public string FrsVersion { get; set; }

    [XmlElement(ElementName = "Status")] public string Status { get; set; }

    [XmlElement(ElementName = "StatusDescription")]
    public StatusDescription StatusDescription { get; set; }

    [XmlAttribute(AttributeName = "RequestType")]
    public string RequestType { get; set; }

    [XmlAttribute(AttributeName = "OverallResult")]
    public string OverallResult { get; set; }

    [XmlAttribute(AttributeName = "RequestID")]
    public int RequestId { get; set; }

    [XmlAttribute(AttributeName = "Version")]
    public double Version { get; set; }
    
}