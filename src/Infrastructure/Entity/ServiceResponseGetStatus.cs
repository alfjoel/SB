namespace Infrastructure.Entity;

public class ServiceResponseGetStatus: EntityBase
{
    public string MerchantID { get; set; }
    public string FRSMerchantID { get; set; }
    public string FRSName { get; set; }
    public string FRSVersion { get; set; }
    public string FRSBuild { get; set; }
    public string Status { get; set; }
    public string StatusDescription { get; set; }
}