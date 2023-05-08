using NetMQ;
using SB.Infrastructure.Entity;

namespace SB.Fiscal.UseCase;

public class GetStatus
{
    private ServiceRequest _fiscalReceipt;
    private List<PrinterDest> _printers;
    public GetStatus(ServiceRequest fiscalReceipt, List<PrinterDest> printers)
    {
        _fiscalReceipt = fiscalReceipt;
        _printers = printers;
    }
    public async Task Run(NetMQSocket socket, CancellationToken stoppingToken)
    {
        
    }
    
    
}