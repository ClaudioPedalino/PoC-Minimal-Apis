public record DeletePlaceCommand(Guid Id) : IRequest<CommandResult> { }

public class DeletePlaceCommandHandler : IRequestHandler<DeletePlaceCommand, CommandResult>
{
    private readonly IMapper _mapper;
    private readonly IPlaceRepository _placeRepository;

    public DeletePlaceCommandHandler(IMapper mapper, IPlaceRepository placeRepository)
    {
        _mapper = mapper;
        _placeRepository = placeRepository;
    }

    public async Task<CommandResult> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
        => await _placeRepository.Delete(request.Id)
        ? CommandResult.Success()
        : CommandResult.Error("Huno un problema al borrar el registro");
}