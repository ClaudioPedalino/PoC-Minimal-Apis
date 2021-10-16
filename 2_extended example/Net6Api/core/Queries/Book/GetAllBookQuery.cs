public record GetAllBookQuery : IRequest<IEnumerable<GetBookResponse>>
{
    public string CacheKey => $"{GetType().Name}";
}

public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQuery, IEnumerable<GetBookResponse>>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public GetAllBookQueryHandler(IMapper mapper, IBookRepository bookRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<GetBookResponse>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
    {
        var result = await _bookRepository.GetAll();

        var response = _mapper.Map<IEnumerable<GetBookResponse>>(result);

        return response;
    }
}