using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Sample.Incident.Data.Models;
using Sample.Incident.Logic.Imports;
using Sample.Incident.Logic.Models;

namespace Sample.Incident.Data
{
    /// <summary>
    /// Implementation of <see cref="IIncidentRepository"/> based on local files
    /// </summary>
    public class IncidentFileRepository : IIncidentRepository
    {
        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();
        private const string DataFilesFolder = "data";

        /// <inheritdoc />
        public async Task<IIncidentInfo> GetAsync(string incidentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            // We don't let the input value potentially traverse/exploit the file system,
            // so let's check it's a valid file name without a path or any illegal characters
            if (string.IsNullOrWhiteSpace(incidentId) || InvalidFileNameChars.Intersect(incidentId).Any())
                return new IncidentInfo();

            // Generate the file name
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), DataFilesFolder,
                $"{incidentId}.json");

            // Check if we have the file
            if (!File.Exists(filePath)) return new IncidentInfo();

            // Read file contents into a string
            var rawData = File.ReadAllText(filePath);

            var result= Deserialize(rawData);

            // Make sure id in the data matches
            if (!string.Equals(result.Id, incidentId, StringComparison.Ordinal))
                throw new IncidentDataException($"File name {filePath} contains data with different incident id {result.Id}");

            return result;
        }

        private static IncidentInfo Deserialize(string incidentRawData)
        {
            var json = JObject.Parse(incidentRawData);

            var description = json["description"];
            var address = json["address"];

            var startedOnUtc = DateTimeOffset.Parse((string) description?["event_opened"]).UtcDateTime;

            return new IncidentInfo(
                (decimal) address?["latitude"],
                (decimal) address?["longitude"],
                startedOnUtc,
                (string) description?["incident_number"],
                incidentRawData
            );
        }
    }
}