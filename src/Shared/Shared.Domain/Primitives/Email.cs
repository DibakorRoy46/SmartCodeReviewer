
namespace Shared.Domain.Primitives;

public record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
            throw new ArgumentException("Invalid email address", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;
}