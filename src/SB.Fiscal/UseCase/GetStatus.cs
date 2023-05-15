using System.Text;
using SB.Hka;
using SB.Infrastructure;
using SB.Infrastructure.Entity;
using SB.Infrastructure.Interfaces;

namespace SB.Fiscal.UseCase;

public class GetStatus
{
    private ServiceRequest _fiscalReceipt;
    private List<PrinterDest> _printers;
    private IClient _socket;

    public GetStatus(ServiceRequest fiscalReceipt, List<PrinterDest> printers,IClient socket)
    {
        _fiscalReceipt = fiscalReceipt;
        _printers = printers;
        _socket = socket;
    }

    public async Task Run(CancellationToken stoppingToken)
    {
        var status = new ServiceResponse
        {
            RequestId = _fiscalReceipt.RequestId,
            RequestType = "GetStatus",
            Version = _fiscalReceipt.Version,
            MerchantId = _fiscalReceipt.MerchantId,
            ZrNumber = _fiscalReceipt.ZrNumber,
            DeviceNumber = _fiscalReceipt.DeviceNumber,
            DeviceType = _fiscalReceipt.DeviceType,
            OverallResult = "Success",
            FrsName = "Alfredo Garcia",
            FrsVersion = "1.0.0",
            StatusDescription = new StatusDescription
            {
                LanguageCode = "en",
                Text = "Online"
            },
            Status = "Idle"
        };

        var findPrinter = _printers.FirstOrDefault(x => x.MachineId == _fiscalReceipt.MerchantId);
        if (findPrinter == null)
        {
            status.Status = "Error";
            status.StatusDescription.Text = "Offline";
            status.OverallResult = "Failure";

            SendStatus(status);
            await Task.Delay(100, stoppingToken);
            return;
        }
        
        using var client = new SocketClient(findPrinter.DestinationIp, findPrinter.PortOfDestination,1000,1000);
        if (!client.Connect())
        {
            status.Status = "Error";
            status.StatusDescription.Text = "Offline";
            status.OverallResult = "Failure";

            SendStatus(status);
            await Task.Delay(100, stoppingToken);
            return;
        }

        var printer = new PrinterHka(client);
        if (!printer.CheckFp())
        {
            status.Status = "Error";
            status.StatusDescription.Text = "Offline";
            status.OverallResult = "Failure";

            SendStatus(status);
            await Task.Delay(100, stoppingToken);
            return;
        }

        SendStatus(status);

        await Task.Delay(100, stoppingToken);
    }
    private void SendStatus(ServiceResponse status)
    {
        string xml = Common.SerializarXml(status);
        Console.WriteLine(xml);
        var byteArray = Encoding.UTF8.GetBytes(xml);
        _socket.Send(byteArray);
    }
}