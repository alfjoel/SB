using Infrastructure;
using Infrastructure.Entity;

namespace Test;

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
        
        var requestGetStatus = Common.DeserializeEntity<ServiceRequestGetStatus>(xml);
        
        Assert.Equal(2055, requestGetStatus.ZrNumber);

    }
}