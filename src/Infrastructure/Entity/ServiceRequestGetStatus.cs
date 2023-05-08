namespace Infrastructure.Entity;

public class ServiceRequestGetStatus: EntityResponse
{
    public string MerchantID { get; set; }
    public DateTime POSTimeStamp { get; set; }
    public string LanguageCode { get; set; }
    
}