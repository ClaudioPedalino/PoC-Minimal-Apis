public record UpdateBookCommand(Guid Id,
                                string Titulo,
                                string Autor,
                                string Genero,
                                decimal Precio,
                                uint CantidadPaginas)
    : IRequest<CommandResponse>, ITransactionable
{ }

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, CommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public UpdateBookCommandHandler(IMapper mapper, IBookRepository bookRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public async Task<CommandResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _bookRepository.GetById(request.Id);
        if (entity is null)
            return CommandResponse.NotFound();

        entity = _mapper.Map<Book>(request);
        await _bookRepository.Update(entity);

        return CommandResponse.Success(CommandResponseHelper.UpdateMessage<Book>());
    }
}