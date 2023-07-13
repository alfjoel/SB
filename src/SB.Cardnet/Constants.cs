namespace SB.Cardnet;

public static class TransactionTypes
{
    public const string NormalPayment = "CN00";
    public const string CardInfo = "CS00";
    public const string VoidPayment = "CN02";
    public const string CloseBatch = "CN01";
}

/// <summary>
/// </summary>
public static class Annotators
{
    public const byte StartOfText = 0x02;
    public const byte EndOfText = 0x03;
    public const byte EndOfTransmission = 0x04;
    public const byte Acknowledge = 0x06;
    public const byte NegativeAcknowledge = 0x15;
    public const byte FieldSeparator = 0x1C;
    public const byte EmptyByte = 0x00;
}

/// <summary>
/// </summary>
internal static class Sizes
{
    public const int Approval = 1;
    public const int Buffer = 1024;
    public const int Error = 4;
}

/// <summary>
/// </summary>
internal static class Timeouts
{
    // ============================
    // Timeouts are in milliseconds
    // ============================

    public const int Approval = 10 * 1000;
    public const int Response = 2 * 60000;
    public const int CloseBatch = 3 * 60000;
}

/// <summary>
/// </summary>
public static class Defaults
{
    public const int MerchantId = -1;
    public const int Folio = -1;
    public const int Deferred = -1;
    public const string Authorization = "";
    public const int AllMerchants = 99;
    public const int BatchSequence = 0;
    public static DateTimeOffset DateTime = new DateTime(1900, 1, 1, 0, 0, 0);

    public static int EmptyInteger = -1;
    public static string EmptyString = "";

    public const string SuccessfulRevocation = "00";
    public const string NoProgressTransaction = "99";
    public const bool WaitForEOT = true;
}

/// <summary>
/// Here is the hosts numbers use at some point inside the void process
/// </summary>
public static class HostNumber
{
    public const int Amex = 08;
}