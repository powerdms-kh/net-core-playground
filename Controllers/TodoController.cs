using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly RedisService _redis;
        public TodoController(IRedisService redis)
        {
            _redis = (RedisService)redis;

            int ViewCount = (int)this._redis.GetDatabase().StringGet("ViewCount");
            this._redis.GetDatabase().StringSet("ViewCount", ViewCount + 1);
        }
    }
}
