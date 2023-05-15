using SB.Hka;
using SB.Infrastructure;
using SB.Infrastructure.Entity;
using SB.Infrastructure.Interfaces;

namespace SB.Fiscal.UseCase;

public class FiscalInvoice
{
    private List<PrinterDest> _printers;
    private IClient _socket;
    private FiscalServiceRequest _fiscalReceipt;
    
    
    public FiscalInvoice(FiscalServiceRequest fiscalReceipt, List<PrinterDest> printers,IClient socket)
    {
        _fiscalReceipt = fiscalReceipt;
        _printers = printers;
        _socket = socket;
    }

    public async Task Run(CancellationToken stoppingToken)
    {

        var price = _fiscalReceipt.TransactionInfo.TotalAmount.Text;
        var name = _fiscalReceipt.Items.ItemInfo.First().Name;
        var findPrinter = _printers.FirstOrDefault(x => x.MachineId == _fiscalReceipt.MerchantId);
        
        if (findPrinter == null)
        {
            return;
        }

        try
        {
            await findPrinter.Semaphore.WaitAsync(stoppingToken);
            using var client = new SocketClient(findPrinter.DestinationIp, findPrinter.PortOfDestination, 1000, 1000);
            var printer = new PrinterHka(client);
            printer.PrintDocument(name, price);


        }
        finally
        {
            findPrinter.Semaphore.Release();
        }
        
    }
}