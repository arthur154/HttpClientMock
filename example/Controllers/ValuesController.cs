using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ExampleHttpClientMock.Service;
using Microsoft.AspNetCore.Mvc;

namespace ExampleHttpClientMock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IHttpClientDependentService _httpClientDependentService;

        public ValuesController (IHttpClientDependentService httpClientDependentService)
        {
            _httpClientDependentService = httpClientDependentService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            string returnValue;

            if (id == 1)
            {
                returnValue = "Some Value";
            }
            else if (id == 2)
            {
                returnValue = "Another Value";
            }
            else
            {
                throw new Exception("No value found.");
            }

            return returnValue;
        }

        // GET api/values/httpClientDependency
        [HttpGet("httpClientDependency")]
        public ActionResult<IEnumerable<string>> GetWithDependency()
        {
            return Ok(_httpClientDependentService.GetValues());
        }

        // GET api/values/httpClientDependency/5
        [HttpGet("httpClientDependency/{id}")]
        public ActionResult<string> GetWithDependency(int id)
        {
            return Ok(_httpClientDependentService.GetValue(id));
        }
    }
}
