public static class ValidationExceptionHandler
{
    public static void UseFluentValidationExceptionHandler(this IApplicationBuilder app)
    {
        var whiteList = new List<string>() { "/hc", "/hc-ui", "/hc-json" };

        app.UseExceptionHandler(x =>
        {
            x.Run(async context =>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = errorFeature?.Error;

                if (whiteList.Contains(context.Request.Path.ToString()))
                {
                    return;
                }

                if (!(exception is ValidationException validationException))
                {
                    throw exception;
                }
                var errors = validationException.Errors.Select(err =>
                    CommandResponse.Fail(err.ErrorMessage));

                var errorText = JsonConvert.SerializeObject(errors);

                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(errorText, Encoding.UTF8);
            });
        });
    }
}