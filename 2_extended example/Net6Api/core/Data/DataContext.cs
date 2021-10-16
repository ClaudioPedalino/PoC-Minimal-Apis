namespace net6.core.Data
{
    public class DataContext : DbContext
    {
        private readonly ILoggerFactory _dataContextLoggerFactory;

        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
            _dataContextLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<People> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_dataContextLoggerFactory)
                .EnableSensitiveDataLogging();

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=localhost;Database=Net6ApiDb;User Id=sa;Password=Temporal1;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            const string requester = "cpedalino";

            foreach (var entityEntry in ChangeTracker.Entries())
            {
                if (entityEntry.IsDelete())
                    entityEntry.SetDeleteAudit(requester);
                else if (entityEntry.IsUpdate())
                    entityEntry.SetUpdateAudit(requester);
                else if (entityEntry.IsNew())
                    entityEntry.SetCreateAudit(requester);
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
