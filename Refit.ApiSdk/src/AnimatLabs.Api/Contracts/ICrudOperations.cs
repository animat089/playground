namespace AnimatLabs.Api.Contracts;

/// <summary>
/// Interface for the CRUD operations
/// </summary>
/// <typeparam name="T">Type of the entity for CRUD operations</typeparam>
public interface ICrudOperations<T>
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetAsync(Guid id);

    Task<Guid?> CreateAsync(T entity);

    Task<bool> UpdateAsync(Guid id, T entity);

    Task<bool> DeleteAsync(Guid id);
}