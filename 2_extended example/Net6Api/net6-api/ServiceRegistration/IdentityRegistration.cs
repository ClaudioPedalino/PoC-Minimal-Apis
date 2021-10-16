public static class IdentityRegistration
{
    public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder, IConfiguration _configuration)
    {
        builder.Services.AddDefaultIdentity<User>()
                        .AddEntityFrameworkStores<DataContext>();

        var jwtSettings = new JwtSettings();
        _configuration.Bind(nameof(jwtSettings), jwtSettings);
        builder.Services.AddSingleton(jwtSettings);

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });

        /// Para definir auth de jwt en todos los endpoints
        //builder.Services.AddAuthorization(opt =>
        //{
        //    opt.FallbackPolicy = new AuthorizationPolicyBuilder()
        //    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        //    .RequireAuthenticatedUser()
        //    .Build();

        //});

        return builder;
    }
}
