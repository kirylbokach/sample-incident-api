using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;

namespace Sample.Incident.IntegrationTests
{
    public class IncidentApiIntegrationTests
    {
        [Theory]
        [ClassData(typeof(IncidentTestCases))]
        public void IncidentsGet_GivenValidIncidentId_ShouldReturnExpectedIncidentAndWeatherInfo(IncidentTestCase testCase)
        {
            var client = new RestClient("http://localhost:57892/api");
            var request = new RestRequest($"incidents/{testCase.Id}");

            var response = client.Get(request);

            Assert.True(response.IsSuccessful);
            var resultJson = JObject.Parse(response.Content);

            // Make sure returned weather is the same as expected for this test case
            Assert.Equal(testCase.ExpectedWeather.ToString(), resultJson["weather"].ToString());

            resultJson.Remove("weather");

            // Make sure returned incident info is the same as expected for this test case
            Assert.Equal(testCase.ExpectedIncident.ToString(), resultJson.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData("12345")]
        public void IncidentsGet_GivenUnknownIncidentId_ShouldReturnNotFound(string id)
        {
            var client = new RestClient("http://localhost:57892/api");
            var request = new RestRequest($"incidents/{id}");

            var response = client.Get(request);

            Assert.False(response.IsSuccessful);
            Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        }
    }

    public class IncidentTestCase
    {
        public JObject ExpectedIncident { get; set; }

        public JObject ExpectedWeather { get; set; }

        public string Id { get; set; }
    }

    public class IncidentTestCases : IEnumerable<object[]>
    {
        private readonly string[] _ids = {"F01705150050", "F01705150090"};

        /// <inheritdoc />
        public IEnumerator<object[]> GetEnumerator()
        {
            return _ids.Select(id => new object[] {
                new IncidentTestCase
                {
                    Id = id,
                    ExpectedIncident = GetIncidentResponse(id),
                    ExpectedWeather = GetWeatherResponse(id)
                }
            }).GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static JObject GetIncidentResponse(string id)
        {
            return JObject.Parse(GetEmbeddedResource($"{id}.json"));
        }

        private static JObject GetWeatherResponse(string id)
        {
            return JObject.Parse(GetEmbeddedResource($"{id}_Weather.json"));
        }

        private static string GetEmbeddedResource(string name)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"Sample.Incident.IntegrationTests.{name}"))
            {
                if (stream == null) throw new Exception($"Unable to find embedded resource {name}");

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
