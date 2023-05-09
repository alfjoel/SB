namespace SB.Hka.Entity;

public static class Constants
{
    public const byte Stx = 2;
    public const byte Etx = 3;
    public const byte Eot = 4;
    public const byte Enq = 5;
    public const byte Ack = 6;
    public const byte So = 14;
    public const byte Nak = 15;
    public const byte Etb = 17;
    
    public const int MaxChar = 40;
    
    public const string CommandVatId = "iR0";
    public const string CommandNcf = "F";
    public const string CommandTypeDocument = "/";
    public const string CommandNameCustomer = "iS0";
    public const string CommandAdditionalInformation = "i";
    public const string CommandCommentInformation = "@";

    public const string CommandExento = " ";
    public const string CommandTasa1 = "!";
    public const string CommandTasa2 = "\"";
    public const string CommandTasa3 = "#";

    public const string SubtotalImpreso = "3";
    public const string PagoDirecto = "101";

    public const string ClosetDocument = "199";
    
}