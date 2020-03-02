using System;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using Sample.Incident.Gateway.Models;
using Sample.Incident.Logic.Imports;
using Sample.Incident.Logic.Models;

namespace Sample.Incident.Gateway
{
    /// <summary>
    /// Implementation of <see cref="IWeatherInfoGateway"/> using https://api.darksky.net
    /// </summary>
    public class DarkSkyWeatherInfoGateway : IWeatherInfoGateway
    {
        private readonly string _serviceEndpoint = "https://api.darksky.net/forecast";
        
        // TODO: Move the secret outside of the source code and into a safer place
        private readonly string _serviceSecret = "a781c7274e4ffec3d5a5bedb718bfcd7";
        
        /// <inheritdoc />
        public async Task<IWeatherInfo> GetHistoricalWeatherInfoAsync(decimal latitude, decimal longitude, DateTime dateTimeUtc,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = new RestClient($"{_serviceEndpoint}/{_serviceSecret}");
            var request = new RestRequest($"{latitude:##.######},{longitude:##.######},{dateTimeUtc:s}Z?exclude=hourly,daily,flags,");

            try
            {
                // TODO: Retries can be added here if getting weather is more important than overall latency
                var response = await client.ExecuteGetAsync(request, cancellationToken);

                if (response.IsSuccessful) return new WeatherInfo(response.Content);
            }
            catch
            {
                // TODO: Add logging here 
            }

            return new WeatherInfo();
        }
    }
}
