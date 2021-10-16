namespace net6.core.Commands
{
    public record UpdatePersonCommand(Guid Id,
                                      string Nombre,
                                      string Apellido,
                                      uint Edad,
                                      DateTime FechaNacimiento,
                                      string Email,
                                      string AvatarUrl,
                                      string Telefono)
        : IRequest<CommandResult>
    { }

    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, CommandResult>
    {
        private readonly IMapper _mapper;
        private readonly IPeopleRepository _peopleRepository;

        public UpdatePersonCommandHandler(IMapper mapper, IPeopleRepository peopleRepository)
        {
            _mapper = mapper;
            _peopleRepository = peopleRepository;
        }

        public async Task<CommandResult> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var entity = await _peopleRepository.GetById(request.Id);
            if (entity is null)
                return CommandResult.NotFound();

            entity = _mapper.Map<People>(request);
            await _peopleRepository.Update(entity);

            return CommandResult.Success();
        }
    }
}

