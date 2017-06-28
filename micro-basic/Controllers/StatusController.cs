using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

#if (EnableNLog)
using NLog.Targets;
using NLog;
#endif

namespace MicroserviceCore.Controllers
{
    [Route("status/")]
    public class StatusController : Controller
    {
        [HttpGet]
        [Route("logs")]
        public IEnumerable<string> Logs()
        {
#if (EnableNLog)
            var target = (MemoryTarget)LogManager.Configuration.FindTargetByName("memory");
            return target.Logs;
#else
            return  new string[] { "Configure logging factory. Logs Disabled." };
#endif
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