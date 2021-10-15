namespace simple.shared.Wrappers
{
    public class CommandResult
    {
        public CommandResult()
        {
            HasErrors = false;
            Message = string.Empty;
        }

        public virtual bool HasErrors { get; set; }
        public virtual string Message { get; set; }

        public static string GenericErrorMessage { get; private set; } = "Hubo un problema al procesar la solicitud";


        public static CommandResult Success()
            => new CommandResult() { HasErrors = false, Message = string.Empty };

        public static CommandResult Success(string message)
            => new CommandResult() { HasErrors = false, Message = message };

        public static CommandResult Error(string message)
            => new CommandResult() { HasErrors = true, Message = message };

        public static CommandResult NotFound()
            => new CommandResult() { HasErrors = true, Message = "No se encontró un registro con la información enviada" };
    }
}
