using SB.Cardnet.Interfaces;

namespace SB.Cardnet.Entity;

public class PaymentTransaction : Transaction
{
    public string Type { get; set; }
    private decimal Amount { get; }
    private decimal Taxes { get; }
    private decimal OtherTaxes { get; }
    private long TransactionId { get; }
    public int Deferred { get; set; }

    public PaymentTransaction(
        decimal amount,
        decimal taxes,
        decimal otherTaxes,
        long transactionId
    )
    {
        Type = TransactionTypes.NormalPayment;
        Amount = amount;
        Taxes = taxes;
        OtherTaxes = otherTaxes;
        TransactionId = transactionId;
    }

    public byte[] ToByteArray()
    {
        var payload = new List<byte> { Annotators.StartOfText };
        Utilities.AddElementToArray(ref payload, 4, Type);

        Utilities.AddElementToArray<long>(ref payload, 12, Utilities.FromDecimalToInt(Amount, 2));
        Utilities.AddElementToArray<long>(ref payload, 12, Utilities.FromDecimalToInt(Taxes, 2));
        Utilities.AddElementToArray<long>(ref payload, 12, Utilities.FromDecimalToInt(OtherTaxes, 2));
        Utilities.AddElementToArray(ref payload, 6, TransactionId, isLast: true);
        payload.Add(Annotators.EndOfText);

        return payload.ToArray();
    }
}

public class PaymentResponse : Response
{
    public string Type { get; set; }
    public int Host { get; set; }
    public string CardType { get; set; }
    public string TransactionMode { get; set; }
    public string CardNumber { get; set; }
    public int BatchNumber { get; set; }
    public int Date { get; set; }
    public int Time { get; set; }
    public string CardName { get; set; }
    public string Approval { get; set; }
    public int PosNumber { get; set; }
    public int RefNumber { get; set; }
    public long Rrn { get; set; } // Retrieval reference number
    public long Commerce { get; set; }
    public string Deferred { get; set; }
    public string TId { get; set; } // Transaction identifier
    public string AId { get; set; } // Application identifier
    public string Signature { get; set; }

    public PaymentResponse(Transaction transaction, byte[] data)
    {
        Type = transaction.Type;
        var response = Utilities.TrimArray(data);

        Host = Utilities.GetElementFromArray(ref response, 2, (short)Defaults.EmptyInteger, Convert.ToInt16);
        CardType = Utilities.GetElementFromArray(ref response, 8, Defaults.EmptyString, Utilities.StringConverter);
        TransactionMode =
            Utilities.GetElementFromArray(ref response, 3, Defaults.EmptyString, Utilities.StringConverter);
        CardNumber = Utilities.GetElementFromArray(ref response, 16, Defaults.EmptyString, Utilities.StringConverter);
        BatchNumber = Utilities.GetElementFromArray(ref response, 3, (short)Defaults.EmptyInteger, Convert.ToInt16);
        Date = Utilities.GetElementFromArray(ref response, 6, Defaults.EmptyInteger, Convert.ToInt32);
        Time = Utilities.GetElementFromArray(ref response, 6, Defaults.EmptyInteger, Convert.ToInt32);
        CardName = Utilities.GetElementFromArray(ref response, 26, Defaults.EmptyString, Utilities.StringConverter);
        Approval = Utilities.GetElementFromArray(ref response, 6, Defaults.EmptyString, Utilities.StringConverter);
        PosNumber = Utilities.GetElementFromArray(ref response, 8, Defaults.EmptyInteger, Convert.ToInt32);
        RefNumber = Utilities.GetElementFromArray(ref response, 3, (short)Defaults.EmptyInteger, Convert.ToInt16);
        Rrn = Utilities.GetElementFromArray(ref response, 12, Defaults.EmptyInteger, Convert.ToInt64);
        Commerce = Utilities.GetElementFromArray(ref response, 15, Defaults.EmptyInteger, Convert.ToInt64);
        Deferred = Utilities.GetElementFromArray(ref response, 4, Defaults.EmptyString, Utilities.StringConverter);
        TId = Utilities.GetElementFromArray(ref response, 15, Defaults.EmptyString, Utilities.StringConverter);
        AId = Utilities.GetElementFromArray(ref response, 16, Defaults.EmptyString, Utilities.StringConverter);

        Utilities.GetElementFromArray(ref response, 64, Defaults.EmptyString,
            Utilities.StringConverter); // Dumps reserved section of the response

        // This also might be 32 due to the shitty documentation for this
        Signature = Utilities.GetElementFromArray(ref response, 41, Defaults.EmptyString, Utilities.StringConverter);
    }
}