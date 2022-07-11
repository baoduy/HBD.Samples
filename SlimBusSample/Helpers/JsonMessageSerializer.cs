using System.Text.Json;
using SlimMessageBus.Host.Serialization;

namespace SlimBusSample.Helpers;

public class JsonMessageSerializer: IMessageSerializer
{
    public JsonSerializerOptions Options { get; set; } = new(JsonSerializerDefaults.Web);
    
    public byte[] Serialize(Type t, object message) => JsonSerializer.SerializeToUtf8Bytes(message, t, Options);

    public object Deserialize(Type t, byte[] payload) => JsonSerializer.Deserialize(payload, t, Options)!;
}