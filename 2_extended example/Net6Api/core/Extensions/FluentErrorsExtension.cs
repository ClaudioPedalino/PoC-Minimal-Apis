public static class FluentErrorsExtension
{
    public static CommandResponse GetCommandResultErrors(this FluentValidation.Results.ValidationResult? validationResult) =>
        CommandResponse.Fail(
            string.Join(',',
                validationResult?.Errors?.Select(x => new { errors = x.ErrorMessage }))
            );
}