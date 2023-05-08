using System.Xml.Serialization;
using SB.Infrastructure.Interfaces;

namespace SB.Infrastructure.Entity;

public class ServiceRequest : IEmv
{
    [XmlElement(ElementName = "MerchantID")]
    public string MerchantId { get; set; }

    [XmlElement(ElementName = "ZRNumber")] public int ZrNumber { get; set; }

    [XmlElement(ElementName = "DeviceNumber")]
    public int DeviceNumber { get; set; }

    [XmlElement(ElementName = "DeviceType")]
    public int DeviceType { get; set; }

    [XmlElement(ElementName = "POSTimeStamp")]
    public DateTime PosTimeStamp { get; set; }

    [XmlElement(ElementName = "TimeoutResponse")]
    public int TimeoutResponse { get; set; }

    [XmlElement(ElementName = "LanguageCode")]
    public string LanguageCode { get; set; }

    [XmlAttribute(AttributeName = "RequestID")]
    public int RequestId { get; set; }

    [XmlAttribute(AttributeName = "RequestType")]
    public string RequestType { get; set; }

    [XmlAttribute(AttributeName = "Version")]
    public double Version { get; set; }
}