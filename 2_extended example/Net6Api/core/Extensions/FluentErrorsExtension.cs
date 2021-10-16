public static class FluentErrorsExtension
{
    public static CommandResult GetCommandResultErrors(this FluentValidation.Results.ValidationResult? validationResult) =>
        CommandResult.Error(string.Join(',', validationResult?.Errors?.Select(x => new { errors = x.ErrorMessage })));
}