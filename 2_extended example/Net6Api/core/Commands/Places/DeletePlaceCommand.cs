public record DeletePlaceCommand(Guid Id) : IRequest<CommandResponse>, ITransactionable { }

public class DeletePlaceCommandHandler : IRequestHandler<DeletePlaceCommand, CommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IPlaceRepository _placeRepository;

    public DeletePlaceCommandHandler(IMapper mapper, IPlaceRepository placeRepository)
    {
        _mapper = mapper;
        _placeRepository = placeRepository;
    }

    public async Task<CommandResponse> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
        => await _placeRepository.Delete(request.Id)
        ? CommandResponse.Success(CommandResponseHelper.DeleteMessage<Place>())
        : CommandResponse.Fail(CommandResponseHelper.DeleteErrorMessage<Place>());
}