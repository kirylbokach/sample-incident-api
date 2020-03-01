using System.Threading;
using System.Threading.Tasks;
using Sample.Incident.Logic.Models;

namespace Sample.Incident.Logic.Imports
{
    /// <summary>
    /// Contract for incident info repository
    /// </summary>
    public interface IIncidentRepository
    {
        Task<IIncidentInfo> GetAsync(string incidentId,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}