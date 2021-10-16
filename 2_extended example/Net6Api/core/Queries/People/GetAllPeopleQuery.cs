namespace net6.core.Queries.People
{
    public record GetAllPeopleQuery : IRequest<IEnumerable<GetPeopleResponse>>
    {
        public string CacheKey => $"{GetType().Name}";
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
        {
            var result = await _peopleRepository.GetAll();

            var response = _mapper.Map<IEnumerable<GetPeopleResponse>>(result);

            return response;
        }
    }
}
