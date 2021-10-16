namespace net6.core.Commands
{
    public record DeleteBookCommand(Guid Id) : IRequest<CommandResult> { }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, CommandResult>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandler(IMapper mapper, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<CommandResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
            => await _bookRepository.Delete(request.Id)
            ? CommandResult.Success()
            : CommandResult.Error("Huno un problema al borrar el registro");
    }
}
