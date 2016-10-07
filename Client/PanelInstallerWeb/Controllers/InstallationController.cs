using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PanelInstallerWeb.Controllers
{
    [Route("api/[controller]")]
    public class InstallationController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<KeyValuePair<int, string>> Get()
        {
            var installs = new List<KeyValuePair<int,string>>();

            installs.Add(new KeyValuePair<int, string>(1, Get(1)));
            installs.Add(new KeyValuePair<int, string>(2, Get(1)));

            return installs;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var filename = String.Format(@"data\Install_{0}.xml", id);
            string installationText = System.IO.File.ReadAllText(filename);

            return installationText;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
