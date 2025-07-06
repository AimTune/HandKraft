namespace HandKraft.Abstractions.Entities;

/// <summary>
/// Indicates the entity belongs to a tenant in a multi-tenant system, using a <see cref="Guid"/> as the tenant identifier.
/// This non-generic interface inherits from the generic <see cref="ITenant{TTenantId}"/> interface with <c>Guid</c> as the tenant ID type.
/// </summary>
public interface ITenant : ITenant<Guid> { }

/// <summary>
/// Interface for multi-tenant entities with a generic tenant identifier type.
/// </summary>
/// <typeparam name="TTenantId">The type of the tenant identifier.</typeparam>
public interface ITenant<TTenantId>
{
    /// <summary>
    /// Gets the unique identifier of the tenant that owns the entity.
    /// </summary>
    TTenantId TenantId { get; }
}