using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PanelInstallerWeb.Controllers
{
    [Route("api/[controller]")]
    public class VerifyInstallController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Produces("application/xml")]
        public string Get(int id)
        {

            var filename = String.Format(@"data\Install_{0}.xml", id);
            string installationText = System.IO.File.ReadAllText(filename);


            X509Certificate2 privateCert = new X509Certificate2(@"data\public_privatekey.pfx", "password");

            var signedmessage = XmlSignMessage.SignXML.SignMessage(installationText, privateCert);
            return signedmessage.OuterXml;
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
