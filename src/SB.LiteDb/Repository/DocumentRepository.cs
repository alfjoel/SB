using LiteDB;

namespace SB.LiteDb.Repository;

public class DocumentRepository<TEntity> where TEntity : class
{
    protected readonly ILiteCollection<TEntity> DbSet;
    
    protected DocumentRepository(LiteClientConnection clientConnection)
    {
        DbSet = clientConnection.Client.GetCollection<TEntity>(typeof(TEntity).Name);
    }
}