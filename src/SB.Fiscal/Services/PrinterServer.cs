using Microsoft.Extensions.Options;
using NetMQ;
using NetMQ.Sockets;
using SB.Fiscal.UseCase;
using SB.Infrastructure;
using SB.Infrastructure.Entity;
using SB.Infrastructure.Interfaces;

namespace SB.Fiscal.Services;

public class PrinterServer : BackgroundService
{
    private readonly AsyncList<Task> _tasks;
    private readonly ILogger<PrinterServer> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly Config _config;

    public PrinterServer(ILogger<PrinterServer> logger, IServiceScopeFactory serviceScopeFactory,
        IOptions<Config> configOptions)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _config = configOptions.Value;
        _tasks =  new AsyncList<Task>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var server = new ResponseSocket();
        server.Bind("tcp://*:5556");
        server.Bind("tcp://*:5555");

        using var poller = new NetMQPoller { server };
        
        server.ReceiveReady += async (s, a) =>
        {
            var task = Working(a.Socket, stoppingToken);
            await _tasks.AddAsync(task, stoppingToken);
        };
        poller.RunAsync();


        while (!stoppingToken.IsCancellationRequested)
        {
            var completedTasks = (await _tasks.ToArrayAsync(stoppingToken))
                .Where(task => task.IsCompleted || task.IsCanceled || task.IsFaulted)
                .ToList();

            foreach (var socketTask in completedTasks)
            {
                if (!await _tasks.RemoveAsync(socketTask, stoppingToken))
                    continue;

                socketTask.Dispose();
                Console.WriteLine("Remove Task With Socket");
            }

            await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
        }
    }

    private async Task Working(NetMQSocket socket, CancellationToken stoppingToken)
    {
        var message = string.Empty;
        while (!stoppingToken.IsCancellationRequested)
        {
            var messageIn = socket.ReceiveFrameString(out var more);
            message += messageIn;
            if (!more) break;
        } 
        
        var entity = Common.DeserializarXml<IEmv>(message);
        switch (entity)
        {
            case FiscalReceipt statusEmv:
            {
                var getstatus = new GetStatus(statusEmv,_config.Printers);
                await getstatus.Run(socket, stoppingToken);
                break;
            }
        }
        
        socket.SendFrame("World");
    }
}