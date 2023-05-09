using System.Buffers;
using System.Net;
using System.Net.Sockets;
using SB.Infrastructure.Interfaces;

namespace SB.Infrastructure;

public class SocketClient: IClient
{
    private const int KSocketPoolTimeoutMicro = 1_000_000;
    private const int KSocketPoolTimeoutMicro2 = 100_000;
    private const int KSendSocketProbablyFullDelayMillis = 30;
    private const int KReceiveSocketProbablyEmptyDelayMillis = 30;
    private bool _lastread;

    private readonly string? _address;
    private readonly int _port;
    private readonly int _readWriteTimeoutMillis;
    private readonly int _connectTimeoutMillis;
    private Socket? _socket;
    private bool _isDisposedFlag;
    
    public SocketClient(string address, int port, int readWriteTimeoutMillis, int connectTimeoutMillis)
    {
        _address = address;
        _port = port;
        _readWriteTimeoutMillis = readWriteTimeoutMillis;
        _connectTimeoutMillis = connectTimeoutMillis;
    }
    public SocketClient(TcpClient received)
    {
    _socket = received.Client;
    }
    
    public void Dispose()
    {
        Disconnect();
        _socket?.Dispose();
    }

    public bool IsConnected => _socket?.Connected ?? false;
    public bool Connect()
    {
        ThrowIfDisposed();
        if (_socket is not null && IsConnected) return false;
        if (!TryGetIpAddress(_address, out var ip)) return false;
        if (ip is null) return false;

        try
        {
            _socket ??= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.SendTimeout = _readWriteTimeoutMillis;
            _socket.ReceiveTimeout = _readWriteTimeoutMillis;

            // Try to connect to the configured server
            var asyncResult = _socket.BeginConnect(ip, _port, null, null);
            if (!asyncResult.AsyncWaitHandle.WaitOne(_connectTimeoutMillis, true))
            {
                // We weren't able to connect to the specified address and port
                _socket.Close();
                throw new TimeoutException($"Unable to connect to the server '{_address}' at port {_port}");
            }

            // We were able to successfully connect to the server
            _socket.EndConnect(asyncResult);
            return true;
        }
        catch (Exception ex) when (ex is not TimeoutException)
        {
            // ignore
        }

        return _socket?.Connected ?? false;
    }
    
    private bool TryGetIpAddress(string input, out IPAddress? ipAddress)
    {
        ipAddress = null;
        if (string.IsNullOrWhiteSpace(input)) return false;
        if (IPAddress.TryParse(input, out ipAddress)) return true;

        try
        {
            var hostInfo = Dns.GetHostEntry(input);
            ipAddress = hostInfo.AddressList[0];
            return true;
        }
        catch
        {
            // ignore
        }

        return false;
    }

    public void Send(in byte[] input)
    {
        ThrowIfDisposed();
        if (_socket is null || !IsConnected || input.Length == 0) return;
        if (!IsConnected) Connect(); // TODO: Should we force a connection when sending data without connecting before?

        var totalBytesSent = 0;
        var totalBytesToSend = input.Length;
        do
        {
            try
            {
                totalBytesSent += _socket.Send(
                    input,
                    totalBytesSent,
                    totalBytesToSend - totalBytesSent,
                    SocketFlags.None
                );
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode is not (SocketError.WouldBlock or SocketError.IOPending
                    or SocketError.NoBufferSpaceAvailable)) throw;
                // Socket buffer is probably full, wait and try again
                Thread.Sleep(KSendSocketProbablyFullDelayMillis);
            }
        } while (totalBytesSent < totalBytesToSend);
    }

    public byte[] Receive(out int totalBytesReceived)
    {
        ThrowIfDisposed();
        var buffer = ArrayPool<byte>.Shared.Rent(4096);
        using var memoryStream = new MemoryStream();
        totalBytesReceived = 0;

        try
        {
            var next = true;
            _lastread = true;
            do
            {
                if (_socket is null) break;
                try
                {
                    int read;
                    var timeout = _lastread ? KSocketPoolTimeoutMicro : KSocketPoolTimeoutMicro2;

                    // Poll the socket for reception with a timeout
                    if (_socket.Poll(timeout, SelectMode.SelectRead))
                        // if (WaitForData())
                    {
                        // This call will not block
                        _lastread = false;
                        read = _socket.Receive(buffer);
                    }
                    else
                    {
                        // Timed out
                        break;
                    }

                    if (read == 0) break;

                    memoryStream.Write(buffer, 0, read);
                    memoryStream.Flush();
                    totalBytesReceived += read;
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode is SocketError.WouldBlock or SocketError.IOPending
                        or SocketError.NoBufferSpaceAvailable)
                    {
                        // Socket buffer is probably empty, wait and try again
                        Thread.Sleep(KReceiveSocketProbablyEmptyDelayMillis);
                        continue;
                    }

                    next = false;
                }
            } while (_socket.Connected && next);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }

        // Done
        return memoryStream.GetBuffer();
    }

    public void Clear()
    {
        if (_socket is null) return;

        var receiveTimeout = _socket.ReceiveTimeout;
        _socket.ReceiveTimeout = 0;

        try
        {
            Receive(out _);
        }
        catch
        {
            // ignore
        }

        _socket.ReceiveTimeout = receiveTimeout;
    }

    public void Disconnect()
    {
        ThrowIfDisposed();
        DisconnectSocket();
    }
    private void ThrowIfDisposed()
    {
        if (!Volatile.Read(ref _isDisposedFlag)) return;

        var typeName = GetType().Name;
        throw new ObjectDisposedException(typeName);
    }
    
    private void DisconnectSocket()
    {
        if (_socket is null || !IsConnected) return;

        _socket?.Shutdown(SocketShutdown.Both);
        _socket?.Disconnect(true);
    }
}