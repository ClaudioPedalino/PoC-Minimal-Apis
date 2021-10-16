namespace net6.core.Extensions
{
    public static class EntitiesExtension
    {
        public static bool IsNew(this EntityEntry entityEntry)
            => entityEntry.State == EntityState.Added;

        public static bool IsUpdate(this EntityEntry entityEntry)
            => entityEntry.State == EntityState.Modified;

        public static bool IsDelete(this EntityEntry entityEntry)
            => entityEntry.State == EntityState.Deleted;

        public static void SetLastModificationAt(this EntityEntry entityEntry)
        {
            entityEntry.Entity.GetType().GetProperties().FirstOrDefault(x => x.Name == "LastModificationAt")?
                              .SetValue(entityEntry.Entity, DateTimeHelper.GetSystemDate());
        }

        public static void SetCreateAudit(this EntityEntry entityEntry, string createBy)
        {
            entityEntry.Entity.GetType().GetProperties().FirstOrDefault(x => x.Name == "CreateBy")?
                              .SetValue(entityEntry.Entity, createBy);

            entityEntry.SetLastModificationAt();
        }

        public static void SetUpdateAudit(this EntityEntry entityEntry, string updateBy)
        {
            entityEntry.Entity.GetType().GetProperties().FirstOrDefault(x => x.Name == "UpdateBy")?
                              .SetValue(entityEntry.Entity, updateBy);

            entityEntry.SetLastModificationAt();
        }

        public static void SetDeleteAudit(this EntityEntry entityEntry, string deleteBy)
        {
            entityEntry.Entity.GetType().GetProperties().FirstOrDefault(x => x.Name == "DeleteBy")?
                              .SetValue(entityEntry.Entity, deleteBy);

            entityEntry.Entity.GetType().GetProperties().FirstOrDefault(x => x.Name == "IsDelete")?
                              .SetValue(entityEntry.Entity, true);

            entityEntry.SetLastModificationAt();
        }
    }
}
