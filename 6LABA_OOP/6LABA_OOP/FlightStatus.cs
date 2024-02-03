using System.Text.Json.Serialization;

namespace OOP_lab6
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FlightStatus
    {
        OnTime,
        Delayed,
        Cancelled,
        Boarding,
        InFlight,
    }
}