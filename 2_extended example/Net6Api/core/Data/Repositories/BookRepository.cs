public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(DataContext dataContext) : base(dataContext) { }
}