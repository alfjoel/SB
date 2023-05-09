using SB.Hka.Interfaces;

namespace SB.Hka.Entity;

internal class DocumentType : IConvertToSend
{
    private TypeNcf Value { get; set; }
    public DocumentType(TypeNcf ncf)
    {
        Value = ncf;
    }

    public string ToSend()
    {
        return Constants.CommandTypeDocument + (int)Value;
    }
    
    public enum TypeNcf
    {
        NotTax = 0,
        Tax = 1,
    }
}