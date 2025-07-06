namespace HandKraft.Abstractions.Entities;

/// <summary>
/// Represents the base class for Value Objects in Domain-Driven Design (DDD).
/// Value Objects are immutable and compared based on their properties' values rather than identity.
/// This class provides equality logic by comparing components returned from <see cref="GetEqualityComponents"/>.
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// Returns the components of the value object that are used to determine equality.
    /// Derived classes should override this method and use <c>yield return</c> to
    /// return each property that defines the value object's identity.
    /// For example, if the value object has Amount and Currency properties,
    /// both should be yielded here to be included in equality checks.
    /// </summary>
    /// <returns>An enumerable sequence of objects representing equality components.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });
    }

    public static bool operator ==(ValueObject? a, ValueObject? b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject? a, ValueObject? b)
    {
        return !(a == b);
    }
}