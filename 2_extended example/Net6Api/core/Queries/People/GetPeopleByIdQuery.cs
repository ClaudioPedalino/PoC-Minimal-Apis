public record GetPeopleByIdQuery(Guid Id) : IRequest<GetPeopleResponse?>, IApiKeyValidation { }


public class GetPeopleByIdQueryHandler : IRequestHandler<GetPeopleByIdQuery, GetPeopleResponse?>
{
    private readonly IMapper _mapper;
    private readonly IPeopleRepository _peopleRepository;

    public GetPeopleByIdQueryHandler(IMapper mapper, IPeopleRepository peopleRepository)
    {
        _mapper = mapper;
        _peopleRepository = peopleRepository;
    }

    public async Task<GetPeopleResponse?> Handle(GetPeopleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _peopleRepository.GetById(request.Id);

        if (result is null)
            return default;

        return _mapper.Map<GetPeopleResponse>(result);
    }
}