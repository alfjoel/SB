using SB.Hka.Interfaces;

namespace SB.Hka.Entity;

internal class CustomerVatId: IConvertToSend
{
    private string Value { get; set; }
    public CustomerVatId(string vatId)
    {
        Value = vatId;
    }

    public string ToSend()
    {
        return Constants.CommandVatId + (Value.Length <= Constants.MaxChar ? Value : Value[..Constants.MaxChar]);
    }
}