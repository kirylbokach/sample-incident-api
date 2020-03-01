using System.Threading;
using System.Threading.Tasks;
using Sample.Incident.Logic.Models;

namespace Sample.Incident.Logic.Services
{
    /// <summary>
    /// Contract for the incident info provider service
    /// </summary>
    public interface IIncidentInfoProvider
    {
        Task<EnrichedIncidentInfo> GetEnrichedIncidentInfoAsync(string incidentId,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}