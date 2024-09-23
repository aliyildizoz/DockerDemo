using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AspNetCoreElasticsearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        public SearchController()
        {
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Search()
        {
            return [];
        }
    }
}
