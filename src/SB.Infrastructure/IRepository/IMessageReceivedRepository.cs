using SB.Infrastructure.Entity;

namespace SB.Infrastructure.IRepository;

public interface IMessageReceivedRepository
{
    void Insert(MessageReceived document);
    IEnumerable<MessageReceived> GetAllNotSynced();
    void UpdateSync(MessageReceived document);
}