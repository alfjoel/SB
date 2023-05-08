namespace SB.Infrastructure.Entity;

public class PrinterDest
{
    public int PortOfOrigin { get; set; }
    public string DestinationIp { get; set; }
    public string PortOfDestination { get; set; }
    public string TypePrinter { get; set; } = "HKA";
}