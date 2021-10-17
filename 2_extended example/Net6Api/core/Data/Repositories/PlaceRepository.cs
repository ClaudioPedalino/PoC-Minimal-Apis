public class PlaceRepository : BaseRepository<Place>, IPlaceRepository
{
    public PlaceRepository(DataContext dataContext) : base(dataContext) { }
}