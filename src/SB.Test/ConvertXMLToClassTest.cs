using SB.Infrastructure;
using SB.Infrastructure.Entity;
using SB.Infrastructure.Interfaces;

namespace SB.Test;

public class ConvertXMLToClassTest
{
    [Fact]
    public void ServiceRequestGetStatusTest()
    {
        string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
            <ServiceRequest RequestID=""1"" RequestType=""GetStatus"" Version=""1.0"">
            <MerchantID>Aupark-Zilina-01001</MerchantID>
            <ZRNumber>2055</ZRNumber>
            <DeviceNumber>701</DeviceNumber>
            <DeviceType>7</DeviceType>
            <POSTimeStamp>2021-10-15T16:40:37.421+02:00</POSTimeStamp>
            <TimeoutResponse>10</TimeoutResponse>
            <LanguageCode>en</LanguageCode>
            </ServiceRequest>";
        
        var requestGetStatus = Common.DeserializarXml<ServiceRequest>(xml);
        
        Assert.Equal(2055, requestGetStatus.ZrNumber);

    }

    [Fact]
    public void ServiceRequestFiscalInvoice()
    {

        string xml = @"<?xml version=""1.0"" encoding=""windows-1250""?><FiscalServiceRequest RequestID=""19"" RequestType=""FiscalInvoice"" Version=""1.0""><MerchantID>605</MerchantID>
            <ZRNumber>7005</ZRNumber>
            <DeviceNumber>605</DeviceNumber>
            <DeviceType>6</DeviceType>
            <DeviceInvoiceNumber>1514</DeviceInvoiceNumber>
            <ReceiptPrinterWidth>40</ReceiptPrinterWidth>
            <POSTimeStamp>2023-05-14T17:41:43.134</POSTimeStamp>
            <TimeoutResponse>20</TimeoutResponse>
            <LanguageCode>en</LanguageCode>
            <Items><ItemInfo><ArticleId>604</ArticleId>
            <Name>TARJETA 10$</Name>
            <EPAN>02996317005061133500001FFFFF</EPAN>
            <Quantity></Quantity>
            <Amount>25.40</Amount>
            <TaxRate TaxType=""A"">16.00</TaxRate>
            <EntryTime>2023-05-14T17:41:38</EntryTime>
            </ItemInfo></Items><TransactionInfo><TimeStamp>2023-05-14T17:41:38</TimeStamp>
            <TotalAmount Currency=""EUR"">25.40</TotalAmount>
            <PaidAmount Type=""Cash"" Currency=""EUR"">25.40</PaidAmount>
            </TransactionInfo></FiscalServiceRequest>";
        
        
        var requestFiscalInvoice = Common.DeserializarXml<FiscalServiceRequest>(xml);
        Assert.Equal("25.40", requestFiscalInvoice.TransactionInfo.TotalAmount.Text.ToString());
        Assert.Equal("TARJETA 10$", requestFiscalInvoice.Items.ItemInfo.First().Name);
    }

    [Fact]
    public void ConvertGuidtoString()
    {
        Guid guid = Guid.NewGuid();
        var guidString = guid.ToString().Replace("-", "");
        Console.WriteLine(guidString.Substring(0,16));
        Console.WriteLine(guidString.Substring(16,16));
        Assert.Equal(32,guidString.Length);
        
    }
}