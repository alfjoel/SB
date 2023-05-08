namespace SB.Infrastructure.Interfaces;

public interface IEntity: IEquatable<IEntity>
{
    public Guid Id { get; set; } 

    public DateTime Created { get; set; } 

    public DateTime? Sync { get; set; }
}