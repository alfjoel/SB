namespace SB.Infrastructure.Entity;

public class Config
{
    public string Country { get; set; }
    public string Publish { get; set; }
    public TimeSpan ZReportExecutionTime { get; set; } = new TimeSpan(24, 0, 0);
    public int Port { get; set; }
    public List<PrinterDest> Printers { get; set; }
    public Config()
    {
        Printers = new List<PrinterDest>();
    }
}