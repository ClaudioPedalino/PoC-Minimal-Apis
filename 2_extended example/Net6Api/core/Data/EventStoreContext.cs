public class EventStoreContext : DbContext
{
    private readonly DatabaseConfig _databaseConfig;

    public EventStoreContext()
    {

    }
    public EventStoreContext(DbContextOptions<EventStoreContext> options,
                             IOptionsMonitor<DatabaseConfig> databaseConfig) : base(options)
    {
        _databaseConfig = databaseConfig.CurrentValue;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();
        //optionsBuilder.UseNpgsql(_databaseConfig.PostgreSqlDb);
        optionsBuilder.UseNpgsql("Server=localhost;Database=EventStoreDb;Port=5432;User Id=postgres;Password=Temporal1");

        base.OnConfiguring(optionsBuilder);
    }

    public void AddEvent(string message)
    {
        var newEvent = new Event()
        {
            Trace = Guid.NewGuid().ToString(),
            Date = DateTimeHelper.GetSystemDate(),
            Message = message,
            Producer = "user or method producer"
        };

        Events.Add(newEvent);
        base.SaveChanges();
    }

    public DbSet<Event> Events { get; set; }
}
