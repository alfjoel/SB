using SB.Infrastructure.Entity;

namespace SB.Payment;

public class Config
{
    public int Port { get; set; }
    public List<TerminalDest> Printers { get; set; }

    public Config()
    {
        Printers = new List<TerminalDest>();
    }
}