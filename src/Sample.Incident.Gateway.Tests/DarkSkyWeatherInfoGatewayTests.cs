using System;
using System.Threading.Tasks;
using Xunit;

namespace Sample.Incident.Gateway.Tests
{
    public class DarkSkyWeatherInfoGatewayTests
    {
        [Theory]
        [InlineData(39.3600586, -84.309939, "2020-02-28 17:00:00")]
        public async Task GetHistoricalWeatherInfoAsync_GivenValidInput_ShouldSucceed(decimal latitude, decimal longitude, string dateTimeUtc)
        {
            var testObject = new DarkSkyWeatherInfoGateway();

            var result = await testObject.GetHistoricalWeatherInfoAsync(latitude, longitude, DateTime.Parse(dateTimeUtc));

            Assert.NotNull(result);
            Assert.True(result.HasValue);
        }
    }
}
