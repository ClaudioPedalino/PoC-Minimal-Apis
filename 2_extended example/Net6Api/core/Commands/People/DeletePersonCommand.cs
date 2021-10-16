namespace net6.core.Commands
{
    public record DeletePersonCommand(Guid Id) : IRequest<CommandResult> { }

    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, CommandResult>
    {
        private readonly IMapper _mapper;
        private readonly IPeopleRepository _peopleRepository;

        public DeletePersonCommandHandler(IMapper mapper, IPeopleRepository peopleRepository)
        {
            _mapper = mapper;
            _peopleRepository = peopleRepository;
        }

        public async Task<CommandResult> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
            => await _peopleRepository.Delete(request.Id)
            ? CommandResult.Success()
            : CommandResult.Error("Huno un problema al borrar el registro");
    }
}
