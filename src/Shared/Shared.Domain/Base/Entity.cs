
namespace Shared.Domain.Base;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; }

    protected Entity() { }

    protected Entity(TId id)
    {
        Id = id;
    }
}
