public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Task<IQueryable<User>> GetAll()
    {
        return Task.FromResult(_dataContext.Users.AsNoTracking());
    }
}