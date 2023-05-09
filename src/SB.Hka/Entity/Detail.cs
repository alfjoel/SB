using SB.Hka.Interfaces;

namespace SB.Hka.Entity;

internal class Detail : IConvertToSend
{
    public decimal Price { get; set; }
    private decimal Count { get; set; }
    private string Description { get; set; }
    private string Tax { get; set; }

    public Detail(string description, decimal price, decimal count, string tax = Constants.CommandExento)
    {
        Description = description;
        Price = price;
        Count = count;
        Tax = tax;
    }

    public string ToSend()
    {
        return Tax + (Price * 100).ToString("0000000000") + (Count * 1000).ToString("00000000") +
               (Description.Length <= Constants.MaxChar ? Description : Description[..Constants.MaxChar]);
    }
}