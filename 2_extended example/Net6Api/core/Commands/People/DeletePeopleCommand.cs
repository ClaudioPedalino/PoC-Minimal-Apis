public record DeletePeopleCommand(Guid Id) : IRequest<CommandResponse>, ITransactionable { }

public class DeletePopleCommandHandler : IRequestHandler<DeletePeopleCommand, CommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IPeopleRepository _peopleRepository;

    public DeletePopleCommandHandler(IMapper mapper, IPeopleRepository peopleRepository)
    {
        _mapper = mapper;
        _peopleRepository = peopleRepository;
    }

    public async Task<CommandResponse> Handle(DeletePeopleCommand request, CancellationToken cancellationToken)
        => await _peopleRepository.Delete(request.Id)
        ? CommandResponse.Success(CommandResponseHelper.DeleteMessage<People>())
        : CommandResponse.Fail(CommandResponseHelper.DeleteErrorMessage<People>());
}