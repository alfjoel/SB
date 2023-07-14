namespace SB.Cardnet.Interfaces;

public interface Transaction
{
    string Type { get; set; }
    byte[] ToByteArray();
}