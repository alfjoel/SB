using Microsoft.Extensions.Options;
using SB.Hka;
using SB.Infrastructure;
using SB.Infrastructure.Entity;

namespace SB.Fiscal.Services;

public class ReportZBackgroundService : BackgroundService
{
    private readonly ILogger<ReportZBackgroundService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly Config _config;

    public ReportZBackgroundService(ILogger<ReportZBackgroundService> logger, IServiceScopeFactory serviceScopeFactory,
        IOptions<Config> configOptions)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _config = configOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now.TimeOfDay;
            var runTime = _config.ZReportExecutionTime - now;
            await Task.Delay(runTime, stoppingToken);
            await Working(_config.Printers, stoppingToken);
        }
    }

    private async Task Working(List<PrinterDest> printers, CancellationToken stoppingToken)
    {
        foreach (var printerEntity in printers)
        {
            try
            {
                await printerEntity.Semaphore.WaitAsync(stoppingToken);
                using var client = new SocketClient(printerEntity.DestinationIp, printerEntity.PortOfDestination, 1000,
                    1000);
                if (!client.Connect())
                {
                    _logger.LogInformation("Error in connect to printer {0}:{1}", printerEntity.DestinationIp,
                        printerEntity.PortOfDestination);
                    continue;
                }

                var printer = new PrinterHka(client);
                if (!printer.CheckFp())
                {
                    _logger.LogInformation("printer did not respond to ping {0}:{1}", printerEntity.DestinationIp,
                        printerEntity.PortOfDestination);
                    continue;
                }

                printer.PrintZReport();
                client.Dispose();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in ReportZBackgroundService");
            }
            finally
            {
                printerEntity.Semaphore.Release();
            }
        }
    }
}