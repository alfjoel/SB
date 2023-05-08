using LiteDB;
using SB.Infrastructure.Entity;

namespace SB.LiteDb.Configuration;

public static class MessageReceivedMap
{
    public static void Configure(BsonMapper mapper)
    {
        mapper.Entity<MessageReceived>().Id(x => x.Id) // set your document ID
            .Field(x => x.Created, "Created") // rename document field
            .Field(x => x.Sync, "Sync")
            .Field(x => x.Message, "Message")
            .Field(x => x.PortOfOrigin, "PortOfOrigin");
    }
}