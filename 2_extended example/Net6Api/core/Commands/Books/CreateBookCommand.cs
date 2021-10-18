public record CreateBookCommand(string Titulo,
                                string Autor,
                                string Genero,
                                decimal Precio,
                                uint CantidadPaginas)
    : IRequest<CommandResponse>, ITransactionable
{ }

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;
    private readonly INotifierService _notifierService;

    public CreateBookCommandHandler(IMapper mapper, IBookRepository bookRepository, INotifierService notifierService)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
        _notifierService = notifierService;
    }

    public async Task<CommandResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Book>(request);

        await _bookRepository.Insert(entity);

        return CommandResponse.Success(
            CommandResponseHelper.CreatedMessage<Book>(entity.Title));
    }


}