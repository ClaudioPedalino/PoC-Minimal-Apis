namespace net6.core.Data
{
    public class PlaceRepository : BaseRepository<Place>, IPlaceRepository
    {
        public PlaceRepository(DataContext dataContext) : base(dataContext) { }
    }
}
