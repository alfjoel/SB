using SB.Hka.Interfaces;

namespace SB.Hka.Entity;

internal class InfoCustomer : IConvertToSend
{
    public int Line { get; private set; } = 0;
    private string Value { get; set; }

    public InfoCustomer(int line, string info)
    {
        Line = line;
        Value = info;
    }

    public string ToSend()
    {
        return Constants.CommandAdditionalInformation + Line.ToString("00") +
               (Value.Length <= Constants.MaxChar ? Value : Value[..Constants.MaxChar]);
    }
}