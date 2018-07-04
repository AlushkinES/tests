using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exness.PgReq;
using Exness.dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Exness.Controllers
{
    [Route("api/vendor")]
    public  class VendorController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            var execPg = new ExecPg();
			object queryResult;
			queryResult = execPg.ExecQuery(id);



			         
			return JsonConvert.SerializeObject(queryResult);
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
