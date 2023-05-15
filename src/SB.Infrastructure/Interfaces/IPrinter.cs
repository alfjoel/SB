namespace SB.Infrastructure.Interfaces;

public interface IPrinter
{
    public bool CheckFp();
    public void PrintXReport();
    public void PrintZReport();
    public bool PrintDocument(string productName, decimal price);
    
}