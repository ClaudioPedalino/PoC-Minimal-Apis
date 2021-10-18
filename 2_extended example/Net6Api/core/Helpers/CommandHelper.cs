public static class CommandHelper
{
    private static INotifierService _notifier;
    private static IDistributedCache _cache;

    public static void NotifierHelperConfigure(INotifierService notifier, IDistributedCache cache)
    {
        _notifier = notifier;
        _cache = cache;
    }

    public static void Notify(string message)
    {
        _notifier.Notify(message);
    }

    public static void Clean(string key)
    {
        _cache.Remove(key);
    }
}