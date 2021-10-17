public static class IdentityRegistration
{
    public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder, AppConfig appConfig)
    {
        builder.Services
            .AddDefaultIdentity<User>()
            .AddEntityFrameworkStores<DataContext>();

        builder.Services
            .AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appConfig.JwtSettings.Secret)),
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
