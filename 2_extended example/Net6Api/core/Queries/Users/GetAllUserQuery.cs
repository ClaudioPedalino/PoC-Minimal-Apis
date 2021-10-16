public record GetAllUserQuery : IRequest<IEnumerable<GetUserResponse>> { }

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<GetUserResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetAllUserQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<GetUserResponse>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        => _mapper.Map<IEnumerable<GetUserResponse>>(
            await _userRepository.GetAll());
}