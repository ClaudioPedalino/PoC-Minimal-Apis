public record CreatePeopleCommand(string Nombre,
                                  string Apellido,
                                  uint Edad,
                                  DateTime FechaNacimiento,
                                  string Email,
                                  string AvatarUrl,
                                  string Telefono)
    : IRequest<CommandResponse>, ITransactionable
{ }

public class CreatePeopleCommandHandler : IRequestHandler<CreatePeopleCommand, CommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IPeopleRepository _peopleRepository;
    private readonly IValidator<People> _validator;

    public CreatePeopleCommandHandler(IMapper mapper, IPeopleRepository peopleRepository, IValidator<People> validator)
    {
        _mapper = mapper;
        _peopleRepository = peopleRepository;
        _validator = validator;
    }

    public async Task<CommandResponse> Handle(CreatePeopleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<People>(request);
        var validationResult = _validator.Validate(entity);
        if (!validationResult.IsValid)
            return validationResult.GetCommandResultErrors();

        await _peopleRepository.Insert(entity);

        return CommandResponse.Success(
            CommandResponseHelper.CreatedMessage<People>($"{entity.FirstName} {entity.LastName}"));
    }
}