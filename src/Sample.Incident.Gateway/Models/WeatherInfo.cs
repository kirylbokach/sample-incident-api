using Sample.Incident.Logic.Models;

namespace Sample.Incident.Gateway.Models
{
    public class WeatherInfo : IWeatherInfo
    {
        public WeatherInfo(string rawData)
        {
            RawData = rawData;
        }

        public WeatherInfo()
        {
        }
        
        /// <inheritdoc />
        public string RawData { get; }

        /// <inheritdoc />
        public bool HasValue => !string.IsNullOrEmpty(RawData);
    }
}