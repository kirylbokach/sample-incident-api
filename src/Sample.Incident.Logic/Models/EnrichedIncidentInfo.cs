namespace Sample.Incident.Logic.Models
{
    public class EnrichedIncidentInfo
    {
        public IIncidentInfo IncidentInfo { get; set; }
        public IWeatherInfo WeatherInfo { get; set; }
    }
}