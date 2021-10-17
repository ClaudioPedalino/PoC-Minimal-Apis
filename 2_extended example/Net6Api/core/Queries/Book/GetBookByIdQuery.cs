public record GetBookByIdQuery(Guid Id) : IRequest<GetBookResponse?>, IApiKeyValidation { }


public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, GetBookResponse?>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public GetBookByIdQueryHandler(IMapper mapper, IBookRepository bookRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public async Task<GetBookResponse?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _bookRepository.GetById(request.Id);

        if (result is null)
            return default;

        return _mapper.Map<GetBookResponse>(result);
    }
}