using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        [HttpGet("db")]
        public IEnumerable<string> TestDb()
        {
            using var db = new SqliteConnection(configuration.GetConnectionString("Context"));
            return db.Query<string>("select Username from Users");
        }

        [HttpGet("createname")]
        public async Task<IActionResult> AddName([FromQuery] string name)
        {
            using var db = new SqliteConnection(configuration.GetConnectionString("Context"));
            var modified = await db.ExecuteAsync("INSERT INTO Users (Username, Password, Salt) VALUES(@Name, 'password', 'salt')", new { Name = name }).ConfigureAwait(false);
            if (modified == 1)
            {
                return Ok(TestDb());
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogError("This is inside the get request");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}