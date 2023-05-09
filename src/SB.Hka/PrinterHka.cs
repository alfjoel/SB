using System.Buffers;
using System.Text;
using SB.Hka.Entity;
using SB.Infrastructure.Interfaces;

namespace SB.Hka;

public class PrinterHka : IPrinter
{
    private readonly IClient _client;

    public PrinterHka(IClient client)
    {
        _client = client;
    }

    public bool CheckFp()
    {
        var data = new[] { Constants.Enq };
        _client.Send(data);
        _client.Receive(out var num);
        if (num > 0) return num >= 5;
        _client.Send(data);
        _client.Receive(out num);
        return num >= 5;
    }

    public void PrintXReport()
    {
        SendCmd("I0X", false);
    }

    public void PrintZReport()
    {
        SendCmd("I0Z", false);
    }

    public void PrintDocument()
    {
    }


    private bool SendCmd(string sCmd, bool retry = true)
    {
        var bytes = Encoding.ASCII.GetBytes(sCmd);
        var len = bytes.Length + 3;
        var buffer = ArrayPool<byte>.Shared.Rent(len);
        Array.Copy(bytes, 0, buffer, 1, bytes.Length);
        buffer[0] = Constants.Stx;
        buffer[bytes.Length + 1] = Constants.Etx;
        buffer[bytes.Length + 2] = Convert.ToByte(Do_XOR(sCmd));
        int num;
        byte[] resp;
        var countRetry = 0;
        do
        {
            _client.Send(buffer[..len]);
            resp = _client.Receive(out num);
            if (resp.FirstOrDefault(Constants.Nak) == Constants.Ack || !retry)
                return true;
            countRetry++;
            Thread.Sleep(2500);
        } while (resp.FirstOrDefault(Constants.Nak) == Constants.Nak || countRetry < 2);

        return false;
    }

    private char Do_XOR(string sCmd)
    {
        char[] charArray = sCmd.ToCharArray();
        int num1 = -1;
        for (int index = 0; index < charArray.Length; ++index)
        {
            if (index != 0 || (int)charArray[index] != (int)Constants.Stx)
            {
                if (num1 == -1)
                {
                    num1 = (int)charArray[index];
                }
                else
                {
                    if ((int)charArray[index] == (int)Constants.Etx)
                        return (char)((uint)num1 ^ (uint)Constants.Etx);
                    num1 ^= (int)charArray[index];
                }
            }
        }

        int num2 = num1 ^ (int)Constants.Etx;
        switch (num2)
        {
            case 164:
                num2 = 8364;
                break;
            case 166:
                num2 = 352;
                break;
            case 168:
                num2 = 353;
                break;
            case 180:
                num2 = 381;
                break;
            case 184:
                num2 = 382;
                break;
            case 188:
                num2 = 338;
                break;
            case 189:
                num2 = 339;
                break;
            case 190:
                num2 = 376;
                break;
        }

        return (char)num2;
    }
}