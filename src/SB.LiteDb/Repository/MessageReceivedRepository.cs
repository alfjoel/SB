using Microsoft.Extensions.Logging;
using SB.Infrastructure.Entity;
using SB.Infrastructure.IRepository;

namespace SB.LiteDb.Repository;

public class MessageReceivedRepository:DocumentRepository<MessageReceived>,  IMessageReceivedRepository
{
    private readonly ILogger<MessageReceivedRepository> _logger;
    
    public MessageReceivedRepository(LiteClientConnection clientConnection, ILogger<MessageReceivedRepository> logger) : base(clientConnection)
    {
        _logger = logger;
    }
    
    public void Insert(MessageReceived document)
    {
        DbSet.Insert(document);
    }

    public IEnumerable<MessageReceived> GetAllNotSynced()
    {
        return DbSet.Find(x => x.Sync == null);
    }

    public void UpdateSync(MessageReceived document)
    {
        DbSet.Update(document);
    }


}