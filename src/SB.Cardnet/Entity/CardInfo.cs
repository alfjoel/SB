namespace SB.Cardnet.Entity;

public class CardInfo
{
    public int Host { get; private set; }
    public string ReadMode { get; private set; }
    public string Number { get; private set; }
    public int ServiceCode { get; private set; }
    public string CardType { get; private set; }
    public string CardName { get; private set; }
    public string Type { get; private set; }

    public CardInfo(IEnumerable<byte> data)
    {
        Type = TransactionTypes.CardInfo;
        var response = Utilities.TrimArray(data);

        Host = Utilities.GetElementFromArray(ref response, 2, (short)Defaults.EmptyInteger, Convert.ToInt16);
        CardType = Utilities.GetElementFromArray(ref response, 8, Defaults.EmptyString, Utilities.StringConverter);
        ReadMode = Utilities.GetElementFromArray(ref response, 3, Defaults.EmptyString, Utilities.StringConverter);
        Number = Utilities.GetElementFromArray(ref response, 8, Defaults.EmptyString, Utilities.StringConverter);
        ServiceCode = Utilities.GetElementFromArray(ref response, 3, (short)Defaults.EmptyInteger, Convert.ToInt16);
        CardName = Utilities.GetElementFromArray(ref response, 26, Defaults.EmptyString, Utilities.StringConverter);
    }
}