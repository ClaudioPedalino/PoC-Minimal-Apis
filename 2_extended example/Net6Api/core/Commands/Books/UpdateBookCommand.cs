public record UpdateBookCommand(Guid Id,
                                string Titulo,
                                string Autor,
                                string Genero,
                                decimal Precio,
                                uint CantidadPaginas)
    : IRequest<CommandResult>
{ }

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, CommandResult>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public UpdateBookCommandHandler(IMapper mapper, IBookRepository bookRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public async Task<CommandResult> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _bookRepository.GetById(request.Id);
        if (entity is null)
            return CommandResult.NotFound();

        entity = _mapper.Map<Book>(request);
        await _bookRepository.Update(entity);

        return CommandResult.Success();
    }
}