using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Sample.Incident.Logic.Services;

namespace Sample.Incident.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentInfoProvider _incidentInfoProvider;

        public IncidentsController(IIncidentInfoProvider incidentInfoProvider)
        {
            _incidentInfoProvider = incidentInfoProvider;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _incidentInfoProvider.GetEnrichedIncidentInfoAsync(id, cancellationToken);

                if (!result.IncidentInfo.HasValue) return NotFound();

                // Serialize the response
                var json = JObject.Parse(result.IncidentInfo.RawData);
                json.Add("weather", JToken.Parse(result.WeatherInfo.RawData));

                return Ok(json);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }
    }
}
