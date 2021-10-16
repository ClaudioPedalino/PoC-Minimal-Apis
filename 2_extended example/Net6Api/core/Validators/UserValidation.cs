public static class UserValidation
{
    public static AuthenticationResult ReturnUserException(string validationMessage)
        => new(string.Empty, new[] { validationMessage });
}