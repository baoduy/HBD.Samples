namespace SlimBusSample.Helpers;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class BusMessagePropertiesAttribute : Attribute
{
    public BusMessagePropertiesAttribute(params string[] properties) => Properties = properties;

    public string[] Properties { get; set; }
}