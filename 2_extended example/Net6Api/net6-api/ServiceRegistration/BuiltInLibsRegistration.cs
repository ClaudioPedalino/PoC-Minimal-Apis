public static class BuiltInLibsRegistration
{
    public static WebApplicationBuilder AddBuiltInLibs(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions();
        builder.Services.AddMemoryCache();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddHttpContextAccessor();

        return builder;
    }
}