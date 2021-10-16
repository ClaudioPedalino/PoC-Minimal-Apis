namespace net6.core.Commands
{
    public record DeleteBookCommand(Guid Id) : IRequest<CommandResult> { }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, CommandResult>
    {
        private readonly IMapper _mapper;
        private readonly IPeopleRepository _peopleRepository;

        public DeleteBookCommandHandler(IMapper mapper, IPeopleRepository peopleRepository)
        {
            _mapper = mapper;
            _peopleRepository = peopleRepository;
        }

        public async Task<CommandResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
            => await _peopleRepository.Delete(request.Id)
            ? CommandResult.Success()
            : CommandResult.Error("Huno un problema al borrar el registro");
    }
}
