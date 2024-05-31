using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text.Json;

namespace AspNetCoreRedis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDosController : ControllerBase
    {
        private readonly IDatabase _database;
        private readonly string ToDosKey = nameof(ToDosKey);

        public ToDosController(IDatabase database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            if (_database.KeyExists(ToDosKey))
            {
                var cacheValue = await _database.ListRangeAsync(ToDosKey);
                return cacheValue.Select(x => x.ToString());
            }

            return [];
        }

        [HttpPost]
        public async Task<string> PostAsync(string todo)
        {
            await _database.ListRightPushAsync(ToDosKey, todo);
            return todo;
        }

        [HttpDelete]
        public async Task<string> DeleteAsync(string todo)
        {
            await _database.ListRemoveAsync(ToDosKey, todo);
            return todo;
        }
    }
}
