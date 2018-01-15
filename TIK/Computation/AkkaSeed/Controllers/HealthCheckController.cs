using System;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace TIK.Computation.AkkaSeed.Controllers
{
    [Route("[Controller]")]
    public class HealthCheckController : Controller
    {
        [HttpGet("")]
        [HttpHead("")]
        public IActionResult Ping()
        {
            Log.Information("This will be written to the rolling file set");
            return Ok();
        }
    }
}
