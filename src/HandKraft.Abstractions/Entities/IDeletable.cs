namespace HandKraft.Abstractions.Entities;

/// <summary>
/// Indicates the entity supports soft deletion.
/// </summary>
public interface IDeletable
{
    /// <summary>
    /// Whether the entity is marked as deleted.
    /// </summary>
    bool IsDeleted { get; }
}