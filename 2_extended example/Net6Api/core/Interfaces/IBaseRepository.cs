public interface IBaseRepository<TEntity> where TEntity : class, new()
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetById(Guid id);
    Task Insert(TEntity entity);
    Task Update(TEntity entity);
    Task<bool> Delete(Guid id);
}