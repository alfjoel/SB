using SB.LiteDb;

namespace SB.Fiscal;

public class LiteDbSetting: ILiteDbSettings
{
    public string ConnectionString { get; set; }
}