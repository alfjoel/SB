using SB.Hka.Interfaces;

namespace SB.Hka.Entity;

internal class InfoComment : IConvertToSend
{
    private string Value { get; set; }

    public InfoComment(string name)
    {
        Value = name;
    }

    public string ToSend()
    {
        return Constants.CommandCommentInformation +
               (Value.Length <= Constants.MaxChar ? Value : Value[..Constants.MaxChar]);
    }
}