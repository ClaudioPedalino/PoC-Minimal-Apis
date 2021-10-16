namespace net6.core.Commands
{
    public record UpdatePlaceCommand(Guid Id,
                                     string Ciudad,
                                     string Direccion,
                                     uint Numeracion,
                                     string Latitud,
                                     string Longitud)
        : IRequest<CommandResult>
    { }

    public class UpdatePlaceCommandHandler : IRequestHandler<UpdatePlaceCommand, CommandResult>
    {
        private readonly IMapper _mapper;
        private readonly IPlaceRepository _placeRepository;

        public UpdatePlaceCommandHandler(IMapper mapper, IPlaceRepository placeRepository)
        {
            _mapper = mapper;
            _placeRepository = placeRepository;
        }

        public async Task<CommandResult> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _placeRepository.GetById(request.Id);
            if (entity is null)
                return CommandResult.NotFound();

            entity = _mapper.Map<Place>(request);
            await _placeRepository.Update(entity);

            return CommandResult.Success();
        }
    }
}

