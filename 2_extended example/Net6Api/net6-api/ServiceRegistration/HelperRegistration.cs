public static class HelperRegistration
{
    public static WebApplication AddHelperConfiguration(this WebApplication app)
    {
        CommandHelper.NotifierHelperConfigure(
    app.Services.GetService<INotifierService>(),
    app.Services.GetService<IDistributedCache>());

        return app;
    }
}