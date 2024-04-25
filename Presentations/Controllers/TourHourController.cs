using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourHourController : ControllerBase
    {
        // GET: api/<TourHourController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TourHourController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TourHourController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TourHourController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TourHourController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
