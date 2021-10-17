public record GetPlaceByIdQuery(Guid Id) : IRequest<GetPlaceResponse?>, IApiKeyValidation { }


public class GetPlaceByIdQueryHandler : IRequestHandler<GetPlaceByIdQuery, GetPlaceResponse?>
{
    private readonly IMapper _mapper;
    private readonly IPlaceRepository _placeRepository;

    public GetPlaceByIdQueryHandler(IMapper mapper, IPlaceRepository placeRepository)
    {
        _mapper = mapper;
        _placeRepository = placeRepository;
    }

    public async Task<GetPlaceResponse?> Handle(GetPlaceByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _placeRepository.GetById(request.Id);

        if (result is null)
            return default;

        return _mapper.Map<GetPlaceResponse>(result);
    }
}