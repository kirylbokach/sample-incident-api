namespace Sample.Incident.Logic.Models
{
    /// <summary>
    /// Model defining the weather info
    /// </summary>
    public interface IWeatherInfo
    {
        string RawData { get; }

        bool HasValue { get; }
    }
}