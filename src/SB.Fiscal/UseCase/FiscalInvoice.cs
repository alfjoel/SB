using System.Text;
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


    public FiscalInvoice(FiscalServiceRequest fiscalReceipt, List<PrinterDest> printers, IClient socket)
    {
        _fiscalReceipt = fiscalReceipt;
        _printers = printers;
        _socket = socket;
    }

    public async Task Run(CancellationToken stoppingToken)
    {
        var response = new FiscalServiceResponse
        {
            RequestId = _fiscalReceipt.RequestId,
            RequestType = "FiscalInvoice",
            Version = _fiscalReceipt.Version,
            MerchantId = _fiscalReceipt.MerchantId,
            ZrNumber = _fiscalReceipt.ZrNumber,
            DeviceNumber = _fiscalReceipt.DeviceNumber,
            DeviceType = _fiscalReceipt.DeviceType,
            OverallResult = "failure",
        };

        var price = _fiscalReceipt.TransactionInfo.TotalAmount.Text;
        var name = _fiscalReceipt.Items.ItemInfo.First().Name;
        var timeout = _fiscalReceipt.TimeoutResponse;
        var findPrinter = _printers.FirstOrDefault(x => x.MachineId == _fiscalReceipt.MerchantId);

        if (findPrinter == null)
        {
            response.OverallResult = "Failure";
            SendResponse(response);
            await Task.Delay(100, stoppingToken);
            return;
        }

        var resultPrint = false;

        try
        {
            await findPrinter.Semaphore.WaitAsync(stoppingToken);
            using var client = new SocketClient(findPrinter.DestinationIp, findPrinter.PortOfDestination, 1000, 1000);
            if (!client.Connect())
            {
                response.OverallResult = "Failure";
                SendResponse(response);
                client.Dispose();
                await Task.Delay(100, stoppingToken);
                return;
            }
            var printer = new PrinterHka(client);
            resultPrint = printer.PrintDocument(name, price);
            client.Dispose();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            findPrinter.Semaphore.Release();
        }

        if (!resultPrint)
        {
            response.OverallResult = "Failure";
            SendResponse(response);
            await Task.Delay(100, stoppingToken);
            return;
        }

        response.OverallResult = "Success";
        var guid = Guid.NewGuid();
        var guidString = guid.ToString().Replace("-", "");
        var bytes = guid.ToByteArray();
        var i = BitConverter.ToInt32(bytes, 0);
        response.Registration = new Registration
        {
            TimeStamp = DateTime.Now,
            FiscalBatch = guidString.Substring(0, 16),
            STAN = guidString.Substring(16, 16),
            FiscalHostCode = guid.ToString(),
            FiscalRegisterCode = guid.ToString(),
            ResponseText = new ResponseText
            {
                LanguageCode = "en",
                Text = "Fiscalised successfully"
            },
            TotalAmount = _fiscalReceipt.TransactionInfo.TotalAmount,
            ServiceID = i,
            Text = ""
        };
        SendResponse(response);
        await Task.Delay(100, stoppingToken);
    }

    private void SendResponse(FiscalServiceResponse response)
    {
        string xml = Common.SerializarXml(response);
        Console.WriteLine(xml);
        var byteArray = Encoding.UTF8.GetBytes(xml);
        _socket.Send(byteArray);
    }
}