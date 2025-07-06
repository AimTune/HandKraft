namespace HandKraft.Abstractions.Entities;

/// <summary>
/// Indicates the entity supports optimistic concurrency control using a row version token of type <see cref="byte[]"/>.
/// This non-generic interface inherits from the generic <see cref="IRowVersion{TRowVersion}"/> interface with <c>byte[]</c> as the row version type.
/// </summary>
public interface IRowVersion : IRowVersion<byte[]> { }

/// <summary>
/// Interface for entities supporting optimistic concurrency control with a generic row version token.
/// </summary>
/// <typeparam name="TRowVersion">The type of the row version token used for concurrency checks.</typeparam>
public interface IRowVersion<TRowVersion>
{
    /// <summary>
    /// Gets the row version token used to handle optimistic concurrency.
    /// </summary>
    TRowVersion RowVersion { get; }
}