using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditController : ControllerBase
    {
        private readonly ILogger<CreditController> _logger;

        public CreditController(ILogger<CreditController> logger)
        {
            _logger = logger;
        }

        [HttpPost("UtilizeCredit")]
        public IActionResult UtilzeCredit()
        {
            return Ok("Raboti");
        }
    }
}
