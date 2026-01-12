
using Shared.Domain.Primitives;

namespace Shared.Domain.Base;

public abstract class AuditableEntity<TId> : Entity<TId>
{
    public UserId CreatedBy { get; private set; } = default!;
    public DateTimeOffset CreatedAt { get; private set; }

    public UserId? ModifiedBy { get; private set; }
    public DateTimeOffset? ModifiedAt { get; private set; }

    protected void CreateAudit(UserId userId)
    {
        CreatedBy = userId;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    protected void UpdateAudit(UserId userId)
    {
        ModifiedBy = userId;
        ModifiedAt = DateTimeOffset.UtcNow;
    }
}
