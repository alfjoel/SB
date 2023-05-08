using SB.Infrastructure.Interfaces;

namespace SB.Infrastructure.Entity;

public class MessageReceived: IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime? Sync { get; set; } = null;
    public string Message { get; set; } = string.Empty;
    public int PortOfOrigin { get; set; } = 0;

    public bool Equals(IEntity? other)
    {
        if (other == null) return false;
        return Id == other.Id;
    }
}