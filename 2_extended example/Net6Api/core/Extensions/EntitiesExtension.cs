public static class EntitiesExtension
{
    private const string DEFAULT_REQUESTER = "_";

    public static bool IsNew(this EntityEntry entityEntry)
        => entityEntry.State == EntityState.Added;

    public static bool IsUpdate(this EntityEntry entityEntry)
        => entityEntry.State == EntityState.Modified;

    public static bool IsDelete(this EntityEntry entityEntry)
        => entityEntry.State == EntityState.Deleted;

    public static void SetDate(this EntityEntry entityEntry)
        => ((BaseEntity)entityEntry.Entity).LastModificationAt = DateTimeHelper.GetSystemDate();

    public static void SetDeleteInfo(this EntityEntry entityEntry, string? requester)
    {
        entityEntry.State = EntityState.Modified;
        ((BaseEntity)entityEntry.Entity).IsDelete = true;
        ((BaseEntity)entityEntry.Entity).DeleteBy = requester ?? DEFAULT_REQUESTER;
    }

    public static void SetCreateInfo(this EntityEntry entityEntry, string? requester)
    {
        ((BaseEntity)entityEntry.Entity).CreateBy = requester ?? DEFAULT_REQUESTER;
    }

    public static void SetUpdateInfo(this EntityEntry entityEntry, string? requester)
    {
        var originalValues = entityEntry.GetDatabaseValues();
        ((BaseEntity)entityEntry.Entity).CreateBy = originalValues.GetValue<string>(nameof(BaseEntity.CreateBy));
        ((BaseEntity)entityEntry.Entity).UpdateBy = requester ?? DEFAULT_REQUESTER;
    }
}