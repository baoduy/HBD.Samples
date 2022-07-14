using MediatR;
using SlimBusSample.Helpers;

namespace SlimBusSample.Models;

public interface IBusMessage
{
    public string? ReplyToSessionId { get; set; }
    public string? ReplyTo { get; set; }
    public string? SessionId { get; set; }
}

[BusMessageProperties(nameof(FilterProperty))]
public class BusMessage :IBusMessage, INotification
{
    public string Body { get; set; } = default!;
    
    public string? ReplyToSessionId { get; set; }
    public string? ReplyTo { get; set; }
    public string? SessionId { get; set; }
    
    public string? FilterProperty { get; set; }
}