public interface ITokenService
{
    AuthenticationResult GenerateAuthResult(IdentityUser newUser);
}