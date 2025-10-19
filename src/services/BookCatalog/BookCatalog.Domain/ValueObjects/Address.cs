namespace BookCatalog.Domain.ValueObjects;

public sealed class Address : IEquatable<Address>
{
    public string Value { get; }

    private Address(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Address cannot be empty.", nameof(value));

        Value = value.Trim();
    }

    public static Address Create(string value)
    {
        return new Address(value);
    }
    
    public bool Equals(Address? other)
    {
        return other != null && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
    }
    
    public override bool Equals(object? obj)
    {
        return Equals(obj as Address);
    }
    
    public override int GetHashCode()
    {
        return Value.GetHashCode(StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(Address address) => address.Value;
}