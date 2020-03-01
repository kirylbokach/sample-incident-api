using System;
using System.Threading;
using System.Threading.Tasks;
using Sample.Incident.Logic.Imports;
using Sample.Incident.Logic.Models;

namespace Sample.Incident.Logic.Services.Implementations
{
    /// <summary>
    /// Implementation of <see cref="IIncidentInfoProvider"/>
    /// </summary>
    public class IncidentInfoProvider : IIncidentInfoProvider
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IWeatherInfoGateway _weatherInfoGateway;

        public IncidentInfoProvider(IIncidentRepository incidentRepository, IWeatherInfoGateway weatherInfoGateway)
        {
            _incidentRepository = incidentRepository ?? throw new ArgumentNullException(nameof(incidentRepository));
            _weatherInfoGateway = weatherInfoGateway ?? throw new ArgumentNullException(nameof(weatherInfoGateway));
        }

        /// <inheritdoc />
        public async Task<EnrichedIncidentInfo> GetEnrichedIncidentInfoAsync(string incidentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Validate the input
            // TODO: Use regex instead
            if (string.IsNullOrWhiteSpace(incidentId)) throw new ArgumentException("Incident Id cannot be empty", nameof(incidentId));

            // Try to populate incident from the repository
            var incidentInfo = await _incidentRepository.GetAsync(incidentId, cancellationToken);

            // If incident not found we just return a blank object
            if (incidentInfo == null) return new EnrichedIncidentInfo();

            // TODO: We need to decide how important the weather info is for this service.
            // We probably don't want to make our service dependent on this external service and should still return 
            // incident data even if we cannot determine the weather conditions
            
            // Obtain the weather info for incident location and time
            var weatherInfo = await _weatherInfoGateway.GetHistoricalWeatherInfoAsync(incidentInfo.Latitude,
                incidentInfo.Longitude, incidentInfo.StartedOnUtc, cancellationToken);
            
            return new EnrichedIncidentInfo { IncidentInfo = incidentInfo, WeatherInfo = weatherInfo };
        }
    }
}