
namespace Shared.Domain.Primitives;

public abstract record TypedId<T>
{
    public Guid Value { get; private set; }
    protected TypedId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Id cannot be empty.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value.ToString();
}