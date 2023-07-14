using SB.Cardnet.Interfaces;

namespace SB.Cardnet.Entity;

public sealed class BatchResponse : Response
{
    public string Type { get; set; }
    public int Result { get; set; }
    public string Host { get; set; }
    public long Commerce { get; set; }
    public int PosNumber { get; set; }
    public int BatchNumber { get; set; }
    public int Date { get; set; }
    public int Time { get; set; }
    public int DevolutionCount { get; set; }

    public decimal DevolutionAmount { get; set; }

    public decimal DevolutionTaxes { get; set; }
    public int Count { get; set; }
    public decimal Amount { get; set; }
    public decimal Taxes { get; set; }
    public decimal OtherTaxes { get; set; }

    public BatchResponse(byte[] data)
    {
        Type = TransactionTypes.CloseBatch;
        var response = Utilities.TrimArray(data);

        Result = Utilities.GetElementFromArray(ref response, 2, (short)Defaults.EmptyInteger, Convert.ToInt16);
        Host = Utilities.GetElementFromArray(ref response, 8, Defaults.EmptyString, Utilities.StringConverter);
        Commerce = Utilities.GetElementFromArray(ref response, 15, Defaults.EmptyInteger, Convert.ToInt64);
        PosNumber = Utilities.GetElementFromArray(ref response, 8, Defaults.EmptyInteger, Convert.ToInt32);
        BatchNumber = Utilities.GetElementFromArray(ref response, 3, (short)Defaults.EmptyInteger, Convert.ToInt16);
        Date = Utilities.GetElementFromArray(ref response, 6, Defaults.EmptyInteger, Convert.ToInt32);
        Time = Utilities.GetElementFromArray(ref response, 6, Defaults.EmptyInteger, Convert.ToInt32);
        DevolutionCount = Utilities.GetElementFromArray(ref response, 3, (short)Defaults.EmptyInteger, Convert.ToInt16);
        DevolutionAmount =
            Utilities.FromIntToDecimal(
                Utilities.GetElementFromArray(ref response, 12, Defaults.EmptyInteger, Convert.ToInt64), 2);
        DevolutionTaxes =
            Utilities.FromIntToDecimal(
                Utilities.GetElementFromArray(ref response, 12, Defaults.EmptyInteger, Convert.ToInt64), 2);
        Count = Utilities.GetElementFromArray(ref response, 3, (short)Defaults.EmptyInteger, Convert.ToInt16);
        Amount = Utilities.FromIntToDecimal(
            Utilities.GetElementFromArray(ref response, 12, Defaults.EmptyInteger, Convert.ToInt64), 2);
        Taxes = Utilities.FromIntToDecimal(
            Utilities.GetElementFromArray(ref response, 12, Defaults.EmptyInteger, Convert.ToInt64), 2);
        OtherTaxes = Utilities.FromIntToDecimal(
            Utilities.GetElementFromArray(ref response, 12, Defaults.EmptyInteger, Convert.ToInt64),
            2);
    }
}