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
    [Route("api/Options")]
    public class OptionsController : Controller
    {
        private AppOptions2 appOptions;

        public OptionsController(IOptions<AppOptions2> options)
        {
            appOptions = options.Value;
        }
        // GET: api/Options
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

       
        
        // POST: api/Options
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Options/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
