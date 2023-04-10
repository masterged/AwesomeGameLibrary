using System.Text.Json.Serialization;

namespace AwesomeGameLibrary.Domain.Audit;


public struct FancyEventType
{
    public required string Value { get; set; }

    public static FancyEventType Type1 { get; } = new() { Value = "Type1" };
    public static FancyEventType Type2 { get; } = new() { Value = "Type2" };

    public override string ToString() => Value;
}