using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestForWebAPI.Models;

namespace TestForWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TestOptionController : Controller
    {
        private AppOptions appOptions;
        private AppOptions2 appOptions2;
        public TestOptionController(IOptions<AppOptions> options)
        {
            appOptions = options.Value;
        }

        public void Get()
        {
        }

        [HttpPost]
        public void Post(IOptions<AppOptions2> options)
        {
            appOptions2 = options.Value;
        }
    }
}