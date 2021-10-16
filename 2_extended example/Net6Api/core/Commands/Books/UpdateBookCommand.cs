namespace net6.core.Commands
{
    public record UpdateBookCommand(Guid Id,
                                      string Nombre,
                                      string Apellido,
                                      uint Edad,
                                      DateTime FechaNacimiento,
                                      string Email,
                                      string AvatarUrl,
                                      string Telefono)
        : IRequest<CommandResult>
    { }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, CommandResult>
    {
        private readonly IMapper _mapper;
        private readonly IPeopleRepository _peopleRepository;

        public UpdateBookCommandHandler(IMapper mapper, IPeopleRepository peopleRepository)
        {
            _mapper = mapper;
            _peopleRepository = peopleRepository;
        }

        public async Task<CommandResult> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
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

