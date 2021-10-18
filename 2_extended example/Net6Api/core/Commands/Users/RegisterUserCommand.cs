public record RegisterUserCommand(string FirstName,
                                  string LastName,
                                  string Email,
                                  string Password)
    : IRequest<AuthenticationResult>, ITransactionable
{ }

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthenticationResult>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public RegisterUserCommandHandler(IMapper mapper, IUserRepository userRepository, UserManager<User> userManager, ITokenService tokenService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<AuthenticationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
            return UserValidation.ReturnUserException(Const.UserAlreadyExist);

        var newUser = _mapper.Map<User>(request);

        var createdUser = await _userManager.CreateAsync(newUser, request.Password);

        return !createdUser.Succeeded
            ? new AuthenticationResult(string.Empty, createdUser.Errors.Select(x => x.Description))
            : _tokenService.GenerateAuthResult(newUser);
    }
}