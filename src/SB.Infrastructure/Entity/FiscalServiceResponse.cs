using System.Xml.Serialization;
using SB.Infrastructure.Interfaces;

namespace SB.Infrastructure.Entity;

[XmlRoot(ElementName = "FiscalServiceResponse")]
public class FiscalServiceResponse : IEmv
{
    [XmlElement(ElementName = "MerchantID")]
    public string MerchantId { get; set; }

    [XmlElement(ElementName = "ZRNumber")] public int ZrNumber { get; set; }

    [XmlElement(ElementName = "DeviceNumber")]
    public int DeviceNumber { get; set; }

    [XmlElement(ElementName = "DeviceType")]
    public int DeviceType { get; set; }

    [XmlElement(ElementName = "DeviceInvoiceNumber")]
    public int DeviceInvoiceNumber { get; set; }

    public bool ShouldSerializeDeviceInvoiceNumber()
    {
        return DeviceInvoiceNumber != 0;
    }

    [XmlElement(ElementName = "Registration")]
    public Registration? Registration { get; set; }

    public bool ShouldSerializeRegistration()
    {
        return Registration != null;
    }

    [XmlElement(ElementName = "FiscalReceipt")]
    public FiscalReceipt? FiscalReceipt { get; set; }

    public bool ShouldSerializeFiscalReceipt()
    {
        return FiscalReceipt != null;
    }

    [XmlAttribute(AttributeName = "RequestType")]
    public string RequestType { get; set; }

    public bool ShouldSerializeRequestType()
    {
        return !string.IsNullOrEmpty(RequestType);
    }

    [XmlAttribute(AttributeName = "OverallResult")]
    public string OverallResult { get; set; }

    [XmlAttribute(AttributeName = "RequestID")]
    public int RequestId { get; set; }

    [XmlAttribute(AttributeName = "Version")]
    public double Version { get; set; }

    [XmlText] public string Text { get; set; }
}