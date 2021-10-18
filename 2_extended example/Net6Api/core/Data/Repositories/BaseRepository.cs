public class BaseRepository<TEntity>
    : IBaseRepository<TEntity>
    where TEntity : BaseEntity, new()
{
    private readonly DataContext _dataContext;
    private readonly DbSet<TEntity> entities;

    public BaseRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
        entities = dataContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAll()
        => await Task.FromResult(entities
        .Where(x => !x.IsDelete)
        .AsNoTracking());

    public async Task<TEntity> GetById(Guid id)
        => await entities
        .AsNoTracking()
        .FirstOrDefaultAsync(entity => entity.Id == id);

    public async Task Insert(TEntity entity)
        => await entities.AddAsync(entity);

    public async Task Update(TEntity entity)
        => entities.Update(entity);

    public async Task<bool> Delete(Guid id)
    {
        TEntity entity = await entities.FirstOrDefaultAsync(entity => entity.Id == id);
        if (entity is not null)
        {
            entity.IsDelete = true;
            //_dataContext.Entry(entity).State = EntityState.Deleted;
            //entities.Update(entity);

            entities.Remove(entity);
            return true;
        }
        return false;
    }
}