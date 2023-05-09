using SB.Hka.Interfaces;

namespace SB.Hka.Entity;

internal class CustomerName : IConvertToSend
{
    private string Value { get; set; }

    public CustomerName(string name)
    {
        Value = name;
    }

    public string ToSend()
    {
        return Constants.CommandNameCustomer + (Value.Length <= Constants.MaxChar ? Value : Value[..Constants.MaxChar]);
    }
}