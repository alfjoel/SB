namespace SB.Infrastructure.Entity;

public class PrinterDest
{
    public int DeviceNumber { get; set; }
    public string DestinationIp { get; set; }
    public int PortOfDestination { get; set; }
    public string TypePrinter { get; set; } = "HKA";
}