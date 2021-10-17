public class NotifierService : INotifierService
{
    private readonly IMediator _mediator;

    public NotifierService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void Notify(string message)
    {
        _mediator.Publish(new NotificationMessage { EventMessage = message });
    }
}

public class NotificationMessage : INotification
{
    public string EventMessage { get; set; }
}

public class NotificationSubscriber1 : INotificationHandler<NotificationMessage>
{
    public Task Handle(NotificationMessage notification, CancellationToken cancellationToken)
    {
        ConsolePrinterHelper.PrintWithColor(
            $"[{nameof(NotificationSubscriber1)}] recibed Event:  {notification.EventMessage}");

        return Task.CompletedTask;
    }
}