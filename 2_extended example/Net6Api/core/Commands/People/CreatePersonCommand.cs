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
    private readonly IValidator<People> _validator;

    public CreatePersonCommandHandler(IMapper mapper, IPeopleRepository peopleRepository, IValidator<People> validator)
    {
        _mapper = mapper;
        _peopleRepository = peopleRepository;
        _validator = validator;
    }

    public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<People>(request);
        var validationResult = _validator.Validate(entity);
        if (!validationResult.IsValid)
            return validationResult.GetCommandResultErrors();

        await _peopleRepository.Insert(entity);

        return CommandResult.Success();
    }
}