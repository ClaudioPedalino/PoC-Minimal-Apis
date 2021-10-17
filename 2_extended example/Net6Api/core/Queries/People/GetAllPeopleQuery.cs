public record GetAllPeopleQuery : IRequest<IEnumerable<GetPeopleResponse>>, IApiKeyValidation, ICacheable
{
    public string CacheKey => $"{GetType().Name}";
    public bool BypassCache { get; set; }
    public TimeSpan? SlidingExpiration { get; set; }
}

public class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, IEnumerable<GetPeopleResponse>>
{
    private readonly IMapper _mapper;
    private readonly IPeopleRepository _peopleRepository;

    public GetAllPeopleQueryHandler(IMapper mapper, IPeopleRepository peopleRepository)
    {
        _mapper = mapper;
        _peopleRepository = peopleRepository;
    }

    public async Task<IEnumerable<GetPeopleResponse>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
        => _mapper.Map<IEnumerable<GetPeopleResponse>>(await _peopleRepository.GetAll());
}