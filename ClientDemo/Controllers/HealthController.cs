using InteraceDemo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelDemo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {

        public HealthController() 
        {
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index() 
        {
            return Content("Health");
        }

    }
}
