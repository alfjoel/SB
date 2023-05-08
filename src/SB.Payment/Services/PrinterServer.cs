using NetMQ;
using NetMQ.Sockets;
using SB.Infrastructure.Entity;

namespace SB.Payment.Services;

public class PrinterServer : BackgroundService
{
    private readonly AsyncList<Task> _tasks = new();

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
        bool more;
        string messageIn = socket.ReceiveFrameString(out more);
        Console.WriteLine("messageIn = {0}", messageIn);
        socket.SendFrame("World");
    }
}