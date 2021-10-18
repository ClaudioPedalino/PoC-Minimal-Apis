public record UpdatePeopleCommand(Guid Id,
                                  string Nombre,
                                  string Apellido,
                                  uint Edad,
                                  DateTime FechaNacimiento,
                                  string Email,
                                  string AvatarUrl,
                                  string Telefono)
    : IRequest<CommandResponse>, ITransactionable
{ }

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePeopleCommand, CommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IPeopleRepository _peopleRepository;

    public UpdatePersonCommandHandler(IMapper mapper, IPeopleRepository peopleRepository)
    {
        _mapper = mapper;
        _peopleRepository = peopleRepository;
    }

    public async Task<CommandResponse> Handle(UpdatePeopleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _peopleRepository.GetById(request.Id);
        if (entity is null)
            return CommandResponse.NotFound();

        entity = _mapper.Map<People>(request);
        await _peopleRepository.Update(entity);

        return CommandResponse.Success(CommandResponseHelper.UpdateMessage<People>());
    }
}