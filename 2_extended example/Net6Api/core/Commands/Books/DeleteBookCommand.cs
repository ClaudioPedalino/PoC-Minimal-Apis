public record DeleteBookCommand(Guid Id) : IRequest<CommandResponse>, ITransactionable { }

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, CommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public DeleteBookCommandHandler(IMapper mapper, IBookRepository bookRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public async Task<CommandResponse> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        => await _bookRepository.Delete(request.Id)
        ? CommandResponse.Success(CommandResponseHelper.DeleteMessage<Book>())
        : CommandResponse.Fail(CommandResponseHelper.DeleteErrorMessage<Book>());
}