public class DataContext : DbContext, IDataContext
{
    private readonly ILoggerFactory _dataContextLoggerFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DatabaseConfig _databaseConfig;
    private IDbContextTransaction _currentTransaction;

    public DataContext() { } /// For migration only

    public DataContext(DbContextOptions<DataContext> options,
                       IHttpContextAccessor httpContextAccessor,
                       IOptionsMonitor<DatabaseConfig> databaseConfig) : base(options)
    {
        Database.EnsureCreated();
        _dataContextLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        _httpContextAccessor = httpContextAccessor;
        _databaseConfig = databaseConfig.CurrentValue;
    }

    public DbSet<Place> Places { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<People> People { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLoggerFactory(_dataContextLoggerFactory)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();

        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer("Server=localhost;Database=Net6ApiDb;User Id=sa;Password=Temporal1;");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<People>().HasQueryFilter(x => !x.IsDelete);
        modelBuilder.Entity<Book>().HasQueryFilter(x => !x.IsDelete);
        modelBuilder.Entity<Place>().HasQueryFilter(x => !x.IsDelete);

        // TODO: Faltan los entity model builders
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var requester = _httpContextAccessor?.HttpContext?.User?.Claims?
            .FirstOrDefault(x => x.Type == Const.UserIdClaim)?.Value;

        var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified
                || e.State == EntityState.Deleted));

        foreach (var entityEntry in entries)
        {
            entityEntry.SetDate();

            if (entityEntry.IsUpdate()) entityEntry.SetUpdateInfo(requester);
            else if (entityEntry.IsNew()) entityEntry.SetCreateInfo(requester);
            else if (entityEntry.IsDelete()) entityEntry.SetDeleteInfo(requester);
        }

        return base.SaveChangesAsync(cancellationToken);
    }


    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        _currentTransaction ??= await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            _currentTransaction?.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _currentTransaction?.RollbackAsync(cancellationToken);
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public async Task RetryOnExceptionAsync(Func<Task> func)
    {
        await Database.CreateExecutionStrategy().ExecuteAsync(func);
    }
}