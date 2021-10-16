public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IConfiguration _configuration;

    public TokenService(JwtSettings jwtSettings, IConfiguration configuration)
    {
        _jwtSettings = jwtSettings;
        _configuration = configuration;
    }

    public AuthenticationResult GenerateAuthResult(IdentityUser newUser)
    {
        var pepe = _configuration.GetSection("JwtSettings:ValidHours");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString("d")),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id),
                    new Claim("createdAt", DateTimeHelper.GetSystemDate().ToString()),
                }),
            Expires = DateTimeHelper.GetSystemDate().AddHours(Convert.ToInt32(_configuration.GetSection("JwtSettings:ValidHours").Value)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                        SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var response = new AuthenticationResult(tokenHandler.WriteToken(token), default);
        return response;
    }

}