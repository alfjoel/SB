namespace SB.Infrastructure.Entity;

public class TerminalDest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Port { get; set; }

    public readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
}