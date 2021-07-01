using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Asp_Core_Web_API_test.Controllers {

    [ApiController]
    //[Route("[controller]")]
    [Route("defaultRoute")]
    public class WeatherForecastController : ControllerBase {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger) {
            _logger = logger;
        }

        [HttpPost]
        public string GetValue () {
            return "Vinícius";
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get() {
            Debug.WriteLine("GET");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => {
                var dt = DateTime.Now.AddDays(index);
                return new WeatherForecast {
                    Date = dt,
                    DateStr = $"{dt:yyyy-MM-dd}T{dt:HH:mm:ss.ff}Z",
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                };
            })
            .ToArray();
        }
    }
}
