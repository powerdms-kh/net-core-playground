using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Model;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly RedisService _redis;
        private readonly TodoContext _context;
        public TodoController(TodoContext context, IRedisService redis)
        {
            _redis = (RedisService)redis;
            _context = context;

            int ViewCount = (int)this._redis.GetDatabase().StringGet("ViewCount");
            this._redis.GetDatabase().StringSet("ViewCount", ViewCount + 1);

            if (_context.TodoItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TodoItems.Add(new TodoItem { name = "Item1" });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> getToDoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }
    }
}