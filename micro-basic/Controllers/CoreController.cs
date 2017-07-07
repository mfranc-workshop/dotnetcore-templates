using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace MicroserviceCore.Controllers
{
    /* 
        This is an example of a Core Logic provided by this service
    */
    public class CoreController: Controller
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public CoreController()
        {
        }

        [HttpGet]
        [Route("core/{id}")]
        public string Micro(int id)
        {
            _logger.Info($"calling with id - {id}");
            return $"Core-{id}";
        }
    }
}