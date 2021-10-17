public static class NotifierHelper
{
    private static INotifierService _notifier;

    public static void NotifierHelperConfigure(INotifierService notifier)
    {
        _notifier = notifier;
    }

    public static void Notify(string message)
    {
        _notifier.Notify(message);
    }
}