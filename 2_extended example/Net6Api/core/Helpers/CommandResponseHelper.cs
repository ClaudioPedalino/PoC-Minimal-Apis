public static class CommandResponseHelper
{
    public static string CreatedMessage<TEntity>(string identifier)
        => $"{typeof(TEntity).FullName} {identifier} succesfully created";

    public static string UpdateMessage<TEntity>()
        => $"{typeof(TEntity).FullName} succesfully updated";

    public static string DeleteMessage<TEntity>()
        => $"{typeof(TEntity).FullName} succesfully deleted";

    public static string CreateErrorMessage<TEntity>()
        => $"There was a problem trying to create current {typeof(TEntity).FullName}";

    public static string UpdateErrorMessage<TEntity>()
        => $"There was a problem trying to update current {typeof(TEntity).FullName}";

    public static string DeleteErrorMessage<TEntity>()
        => $"There was a problem trying to delete current {typeof(TEntity).FullName}";
}