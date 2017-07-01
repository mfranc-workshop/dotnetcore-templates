using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NLog.Targets;
using NLog;

namespace MicroserviceCore.Controllers
{
    [Route("status/")]
    public class StatusController : Controller
    {
        [HttpGet]
        [Route("logs")]
        public IEnumerable<string> Logs()
        {
            var target = (MemoryTarget)LogManager.Configuration.FindTargetByName("memory");
            return target.Logs;
        }

        [HttpGet]
        [Route("health")]
        public JsonResult Health()
        {
            var health = new {
                isAlive = true,
                /* put test for external dependancies */
            };

            return Json(health);
        }
    }
}