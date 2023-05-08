using NetMQ;
using SB.Infrastructure.Entity;

namespace SB.Fiscal.UseCase;

public class GetStatus
{
    private FiscalReceipt _fiscalReceipt;
    private List<PrinterDest> _printers;
    public GetStatus(FiscalReceipt fiscalReceipt, List<PrinterDest> printers)
    {
        _fiscalReceipt = fiscalReceipt;
        _printers = printers;
    }
    public async Task Run(NetMQSocket socket, CancellationToken stoppingToken)
    {
        
    }
    
    
}