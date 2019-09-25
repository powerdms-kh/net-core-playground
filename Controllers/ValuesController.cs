using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly RedisService _redis;
        public ValuesController(
            IRedisService redis
        )
        {
            this._redis = (RedisService)redis;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                int ViewCount = (int)this._redis.GetDatabase().StringGet("ViewCount") + 1;
                this._redis.GetDatabase().StringSet("ViewCount", ViewCount);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            return new string[] { this._redis.GetDatabase().StringGet("ViewCount"), "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
