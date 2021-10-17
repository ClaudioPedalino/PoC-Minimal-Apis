public record GetAllPlaceQuery : IRequest<IEnumerable<GetPlaceResponse>>, IApiKeyValidation, ICacheable
{
    public string CacheKey => $"{GetType().Name}";
    public bool BypassCache { get; set; }
    public TimeSpan? SlidingExpiration { get; set; }
}

public class GetAllPlaceQueryHandler : IRequestHandler<GetAllPlaceQuery, IEnumerable<GetPlaceResponse>>
{
    private readonly IMapper _mapper;
    private readonly IPlaceRepository _placeRepository;

    public GetAllPlaceQueryHandler(IMapper mapper, IPlaceRepository placeRepository)
    {
        _mapper = mapper;
        _placeRepository = placeRepository;
    }

    public async Task<IEnumerable<GetPlaceResponse>> Handle(GetAllPlaceQuery request, CancellationToken cancellationToken) =>
        _mapper.Map<IEnumerable<GetPlaceResponse>>(await _placeRepository.GetAll());
}