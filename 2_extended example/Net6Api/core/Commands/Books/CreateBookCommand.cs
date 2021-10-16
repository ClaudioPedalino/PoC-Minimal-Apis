public record CreateBookCommand(string Titulo,
                                string Autor,
                                string Genero,
                                decimal Precio,
                                uint CantidadPaginas)
    : IRequest<CommandResult>
{ }

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CommandResult>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(IMapper mapper, IBookRepository bookRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public async Task<CommandResult> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Book>(request);

        await _bookRepository.Insert(entity);

        return CommandResult.Success();
    }
}