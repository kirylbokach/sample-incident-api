using System;
using Sample.Incident.Logic.Models;

namespace Sample.Incident.Data.Models
{
    /// <summary>
    /// Implementation of <see cref="IIncidentInfo"/> with underlying JSON object
    /// </summary>
    public class IncidentInfo : IIncidentInfo
    {
        public IncidentInfo(decimal latitude, decimal longitude, DateTime startedOnUtc, string id, string rawData)
        {
            Latitude = latitude;
            Longitude = longitude;
            StartedOnUtc = startedOnUtc;
            Id = id;
            RawData = rawData;
        }

        public IncidentInfo()
        {
        }

        /// <inheritdoc />
        public decimal Latitude { get; }

        /// <inheritdoc />
        public decimal Longitude { get; }

        /// <inheritdoc />
        public DateTime StartedOnUtc { get; }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public string RawData { get; }

        /// <inheritdoc />
        public bool HasValue => !string.IsNullOrEmpty(Id);
    }
}