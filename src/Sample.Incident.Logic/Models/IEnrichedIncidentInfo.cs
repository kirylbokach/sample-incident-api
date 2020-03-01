namespace Sample.Incident.Logic.Models
{
    public interface IEnrichedIncidentInfo : IIncidentInfo
    {
        IWeatherInfo WeatherInfo { get; set; }
    }
}