using Xunit;

namespace Sample.Incident.Data.Tests
{
    public class IncidentFileRepositoryTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("/var/secret")]
        public async void GetAsync_GivenUnknownId_ShouldReturnEmptyIncident(string incidentId)
        {
            var testObject = new IncidentFileRepository();

            var result = await testObject.GetAsync(incidentId);

            Assert.Null(result.Id);
        }

        [Theory]
        [InlineData("F01705150050")]
        [InlineData("F01705150090")]
        public async void GetAsync_GivenKnownId_ShouldReturnIncidentData(string incidentId)
        {
            var testObject = new IncidentFileRepository();

            var result = await testObject.GetAsync(incidentId);

            Assert.Equal(incidentId, result.Id);
        }
    }
}
