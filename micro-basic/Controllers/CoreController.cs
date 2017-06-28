using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroserviceCore.Controllers
{
    /* 
        This is an example of a Core Logic provided by this service
    */
    public class CoreController: Controller
    {
        private ILogger<CoreController> _logger;

        public CoreController(ILogger<CoreController> logger)
        {
           _logger = logger; 
        }

        [HttpGet]
        [Route("core/{id}")]
        public string Micro(int id)
        {
            _logger.LogInformation($"calling with id - {id}");
            return $"Core-{id}";
        }
    }
}