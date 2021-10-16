public interface IUserRepository
{
    Task<IQueryable<User>> GetAll();
}