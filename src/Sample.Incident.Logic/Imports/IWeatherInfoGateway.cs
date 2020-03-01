using System;
using System.Threading;
using System.Threading.Tasks;
using Sample.Incident.Logic.Models;

namespace Sample.Incident.Logic.Imports
{
    /// <summary>
    /// Contract for external weather service
    /// </summary>
    public interface IWeatherInfoGateway
    {
        Task<IWeatherInfo> GetHistoricalWeatherInfoAsync(decimal latitude, decimal longitude, DateTime dateTime,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}