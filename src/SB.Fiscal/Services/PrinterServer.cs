using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Options;
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
        _tasks = new AsyncList<Task>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tcpListener = new TcpListener(IPAddress.Any, _config.Port);
        tcpListener.Start();

        while (!stoppingToken.IsCancellationRequested)
        {
            var tcpClient = await tcpListener.AcceptTcpClientAsync(stoppingToken);
            var task = Working(new SocketClient(tcpClient), stoppingToken);
            await _tasks.AddAsync(task, stoppingToken);
            var completedTasks = (await _tasks.ToArrayAsync(stoppingToken))
                .Where(taskWorking => taskWorking.IsCompleted || taskWorking.IsCanceled || taskWorking.IsFaulted)
                .ToList();

            foreach (var socketTask in completedTasks)
            {
                if (!await _tasks.RemoveAsync(socketTask, stoppingToken))
                    continue;

                socketTask.Dispose();
                Console.WriteLine("Remove Task With Socket");
            }
        }
    }

    private async Task Working(IClient socket, CancellationToken stoppingToken)
    {
        var messageIn = socket.Receive(out var length);
        var message = Encoding.UTF8.GetString(messageIn, 0, length);

        var entity = Common.DeserializarXml<IEmv>(message);
        switch (entity)
        {
            case ServiceRequest statusEmv:
            {
                var status = new GetStatus(statusEmv, _config.Printers, socket);
                await status.Run(stoppingToken);
                break;
            }
        }

        socket.Dispose();
    }
}