public record GetAllPlaceQuery : IRequest<IEnumerable<GetPlaceResponse>>
{
    public string CacheKey => $"{GetType().Name}";
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

    public async Task<IEnumerable<GetPlaceResponse>> Handle(GetAllPlaceQuery request, CancellationToken cancellationToken)
    {
        var result = await _placeRepository.GetAll();

        var response = _mapper.Map<IEnumerable<GetPlaceResponse>>(result);

        return response;
    }
}