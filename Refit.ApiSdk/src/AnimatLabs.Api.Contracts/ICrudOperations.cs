namespace AnimatLabs.Api.Contracts
{
    public interface ICrudOperations<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<Guid?> CreateAsync(TEntity entity);

        public Task<TEntity?> GetAsync(Guid entityId);

        public Task<int> DeleteAsync(Guid entityId);
    }
}