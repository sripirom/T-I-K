using System;
using Microsoft.AspNetCore.Mvc;

namespace TIK.Computation.AkkaSeed.Controllers
{
    [Route("[Controller]")]
    public class HealthCheckController : Controller
    {
        [HttpGet("")]
        [HttpHead("")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
