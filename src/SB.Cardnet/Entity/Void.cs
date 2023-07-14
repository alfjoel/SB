using SB.Cardnet.Interfaces;

namespace SB.Cardnet.Entity;

public class VoidTransaction : Transaction
{
    public string Type { get; set; }
    private int Host { get; }
    private long Folio { get; }
    private int RefNumber { get; }

    public VoidTransaction(
        int host,
        int refNumber,
        long folio = Defaults.Folio
    )
    {
        Type = TransactionTypes.VoidPayment;
        Host = host;
        RefNumber = refNumber;
        Folio = folio;
    }

    public byte[] ToByteArray()
    {
        var payload = new List<byte> { Annotators.StartOfText };

        Utilities.AddElementToArray<string>(ref payload, 4, Type);

        Utilities.AddElementToArray<int>(ref payload, 2, Host);

        if (Folio != Defaults.Folio & Host != HostNumber.Amex) // Pack folio only if the host is not AMEX
        {
            Utilities.AddElementToArray<long>(ref payload, 8, Folio);
        }

        Utilities.AddElementToArray<int>(ref payload, 3, RefNumber, isLast: true);

        payload.Add(Annotators.EndOfText);

        return payload.ToArray();
    }
}

public class VoidResponse : Response
{
    public string Type { get; set; }
    public string Result { get; set; }

    public VoidResponse(Transaction transaction, byte[] data)
    {
        Type = transaction.Type;
        var response = Utilities.TrimArray(data);

        Result = Utilities.GetElementFromArray<string>(ref response, 2, Defaults.EmptyString,
            Utilities.StringConverter);
    }
}