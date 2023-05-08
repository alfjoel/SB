using System.Xml.Serialization;
using SB.Infrastructure.Interfaces;

namespace SB.Infrastructure.Entity;

[XmlRoot(ElementName = "FiscalServiceRequest")]
public class FiscalServiceRequest : IEmv
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

    [XmlElement(ElementName = "DeviceInvoiceNumber")]
    public int DeviceInvoiceNumber { get; set; }

    [XmlElement(ElementName = "ReceiptPrinterWidth")]
    public int ReceiptPrinterWidth { get; set; }

    [XmlElement(ElementName = "Items")] public Items Items { get; set; }

    [XmlElement(ElementName = "TransactionInfo")]
    public TransactionInfo TransactionInfo { get; set; }

    [XmlElement(ElementName = "CustomerInfo")]
    public CustomerInfo CustomerInfo { get; set; }

    [XmlAttribute(AttributeName = "RequestID")]
    public int RequestId { get; set; }

    [XmlAttribute(AttributeName = "RequestType")]
    public string RequestType { get; set; }

    [XmlAttribute(AttributeName = "Version")]
    public double Version { get; set; }

    [XmlText] public string Text { get; set; }
}