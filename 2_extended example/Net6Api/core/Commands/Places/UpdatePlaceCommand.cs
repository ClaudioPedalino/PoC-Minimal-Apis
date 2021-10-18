public record UpdatePlaceCommand(Guid Id,
                                 string Ciudad,
                                 string Direccion,
                                 uint Numeracion,
                                 string Latitud,
                                 string Longitud)
    : IRequest<CommandResponse>, ITransactionable
{ }

public class UpdatePlaceCommandHandler : IRequestHandler<UpdatePlaceCommand, CommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IPlaceRepository _placeRepository;

    public UpdatePlaceCommandHandler(IMapper mapper, IPlaceRepository placeRepository)
    {
        _mapper = mapper;
        _placeRepository = placeRepository;
    }

    public async Task<CommandResponse> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _placeRepository.GetById(request.Id);
        if (entity is null)
            return CommandResponse.NotFound();

        entity = _mapper.Map<Place>(request);
        await _placeRepository.Update(entity);

        return CommandResponse.Success(CommandResponseHelper.UpdateMessage<Place>());
    }
}