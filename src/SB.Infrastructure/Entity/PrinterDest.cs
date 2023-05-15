namespace SB.Infrastructure.Entity;

public class PrinterDest
{
    public string MachineId { get; set; }
    public string DestinationIp { get; set; }
    public int PortOfDestination { get; set; }
    public string TypePrinter { get; set; } = "HKA";
    public readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
}