using System;

namespace Sample.Incident.Data.Models
{
    public class IncidentDataException : Exception
    {
        /// <inheritdoc />
        public IncidentDataException(string message) : base(message)
        {
        }
    }
}