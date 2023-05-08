using LiteDB;

namespace SB.LiteDb;

public class LiteClientConnection
{
    public LiteClientConnection(ILiteDbSettings settings)
    {
        Client = new LiteDatabase(settings.ConnectionString, _bsonMapper);

    }
    public ILiteDatabase Client { get; } = null;
    
    private BsonMapper _bsonMapper
    {
        get
        {
            var bsonMapper = BsonMapper.Global;
            return bsonMapper;
        }
    }
}