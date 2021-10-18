public record LoginUserCommand(string Email, string Password)
    : IRequest<AuthenticationResult>
{ }

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthenticationResult>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(IMapper mapper, IUserRepository userRepository, UserManager<User> userManager, ITokenService tokenService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<AuthenticationResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return UserValidation.ReturnUserException(Const.UserDoesNotExist);

        var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        return !userHasValidPassword
            ? UserValidation.ReturnUserException(Const.UserOrPasswordAreIncorrect)
            : _tokenService.GenerateAuthResult(user);
    }
}