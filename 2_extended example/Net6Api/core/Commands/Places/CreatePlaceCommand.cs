public record CreatePlaceCommand(string Ciudad,
                                 string Direccion,
                                 uint Numeracion,
                                 string Latitud,
                                 string Longitud)
    : IRequest<CommandResult>
{ }

public class CreatePlaceCommandHandler : IRequestHandler<CreatePlaceCommand, CommandResult>
{
    private readonly IMapper _mapper;
    private readonly IPlaceRepository _placeRepository;

    public CreatePlaceCommandHandler(IMapper mapper, IPlaceRepository placeRepository)
    {
        _mapper = mapper;
        _placeRepository = placeRepository;
    }

    public async Task<CommandResult> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Place>(request);

        await _placeRepository.Insert(entity);

        return CommandResult.Success();
    }
}