using SB.Hka.Interfaces;

namespace SB.Hka.Entity;

internal class NcfInfoCustomer: IConvertToSend
{
    private string Value { get; set; }
    public NcfInfoCustomer(string ncf)
    {
        Value = ncf;
    }

    public string ToSend()
    {
        return Constants.CommandNcf + (Value.Length <= Constants.MaxChar ? Value : Value[..Constants.MaxChar]);
    }
}