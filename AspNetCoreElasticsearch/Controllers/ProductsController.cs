using AspNetCoreElasticsearch.Models;
using AspNetCoreElasticsearch.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AspNetCoreElasticsearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ElasticsearchService _elasticsearchService;

        public ProductsController(ElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchByProductName(string productName)
        {
            return Ok(await _elasticsearchService.SearchByProductName(productName));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Product product)
        {
            return Ok(await _elasticsearchService.Create(product));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Product product)
        {
            return Ok(await _elasticsearchService.Update(product));
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(string productId)
        {
            return Ok(await _elasticsearchService.Delete(productId));
        }
    }
}
