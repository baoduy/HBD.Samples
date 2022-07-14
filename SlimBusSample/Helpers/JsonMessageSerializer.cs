using System.Text.Json;
using System.Text.Json.Serialization;
using SlimMessageBus.Host.Serialization;

namespace SlimBusSample.Helpers;

public class JsonMessageSerializer: IMessageSerializer
{
    public JsonSerializerOptions Options { get; set; } = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        AllowTrailingCommas = true
    };
    
    public byte[] Serialize(Type t, object message) => JsonSerializer.SerializeToUtf8Bytes(message, t, Options);

    public object Deserialize(Type t, byte[] payload) => JsonSerializer.Deserialize(payload, t, Options)!;
}