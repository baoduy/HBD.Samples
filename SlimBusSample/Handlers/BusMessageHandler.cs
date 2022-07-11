using MediatR;
using SlimBusSample.Models;
using SlimMessageBus;

namespace SlimBusSample.Handlers;

internal class BusMessageHandler : INotificationHandler<BusMessage>, IConsumer<BusMessage>
{
    public Task Handle(BusMessage notification, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Handled a {nameof(BusMessage)} event: {notification.Body}");
        return Task.CompletedTask;
    }

    public Task OnHandle(BusMessage message, string path) => Handle(message);
}