using System;

namespace Sample.Incident.Logic.Models
{
    /// <summary>
    /// Model, that defines incident info and data elements we care about for business logic
    /// </summary>
    public interface IIncidentInfo
    {
        // We assume that all this data will be available for all incidents we store
        decimal Latitude { get; }
        decimal Longitude { get; }
        DateTime StartedOn { get; }
    }
}