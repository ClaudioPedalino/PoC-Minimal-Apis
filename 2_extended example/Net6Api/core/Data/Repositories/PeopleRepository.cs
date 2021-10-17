public class PeopleRepository : BaseRepository<People>, IPeopleRepository
{
    public PeopleRepository(DataContext dataContext) : base(dataContext) { }
}