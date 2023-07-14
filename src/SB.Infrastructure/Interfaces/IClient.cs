namespace SB.Infrastructure.Interfaces;

public interface IClient: IDisposable
{
    bool IsConnected { get; }

    bool Connect();

    void Send(in byte[] input);

    byte[] Receive(out int totalBytesReceived, int timeoutDefault = 1_000_000);

    void Clear();

    void Disconnect();
}