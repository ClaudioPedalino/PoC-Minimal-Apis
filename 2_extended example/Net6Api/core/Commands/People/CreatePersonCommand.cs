namespace net6.core.Commands
{
    public record CreatePersonCommand(string Nombre,
                                      string Apellido,
                                      uint Edad,
                                      DateTime FechaNacimiento,
                                      string Email,
                                      string AvatarUrl,
                                      string Telefono)
        : IRequest<CommandResult>
    { }

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CommandResult>
    {
        private readonly IMapper _mapper;
        private readonly IPeopleRepository _peopleRepository;

        public CreatePersonCommandHandler(IMapper mapper, IPeopleRepository peopleRepository)
        {
            _mapper = mapper;
            _peopleRepository = peopleRepository;
        }

        public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<People>(request);

            await _peopleRepository.Insert(entity);

            return CommandResult.Success();
        }
    }
}
