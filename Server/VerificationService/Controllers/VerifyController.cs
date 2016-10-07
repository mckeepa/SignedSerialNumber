using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VerificationService.Model;

namespace VerificationService.Controllers
{
    [Route("api/[controller]")]
    public class VerifyController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public VerificationResponse Post([FromBody]Model.VerifyRequest value)
        {
            VerificationResponse _VerificationResponse = new VerificationResponse();
            _VerificationResponse.Installer = new Installer
            {
                CompanyABN = "123456789",
                CompanyName = "Installers R Us",
                FirstName = "John",
                LastName = "Smith",
                ID = 1234
            };
            _VerificationResponse.Products = new List<VerifiedProduct> {
                new VerifiedProduct() {
                    Importer ="abc",
                    FlashTest ="Flash test sample",
                    Manufacturer ="Manufacturer ABC",
                    ModelNumber ="ABC123" ,
                    Retailer = new Organisation {Name="Retaler 1", ABN="21234567" },
                    SerialNumber ="3212334565A" 
                },
                new VerifiedProduct() {
                    Importer = "abc",
                    FlashTest = "Flash test sample 2",
                    Manufacturer = "Manufacturer ABC",
                    ModelNumber = "ABC124",
                    Retailer = new Organisation { Name = "Retaler 1", ABN = "21234567" },
                    SerialNumber = "3212334544A" }
            };


            return _VerificationResponse;
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
