using DockerDemoWebApi.DockerComposeDbContext;
using DockerDemoWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DockerDemoWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DockerComposeDemoDbContext _dockerComposeDbContext;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(ILogger<ProductsController> logger, DockerComposeDemoDbContext dockerComposeDbContext)
        {
            _logger = logger;
            _dockerComposeDbContext = dockerComposeDbContext;
        }

        [HttpGet]
        public async Task<List<Product>> Get()
        {
            return await _dockerComposeDbContext.Products.ToListAsync();
        }

        
        [HttpPost]
        public async Task<Product> Post(ProductDto productDto)
        {
            var product = new Product()
            {
                Name = productDto.Name,
                Weight = productDto.Weight,
                InsertDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _dockerComposeDbContext.Add(product);
            await _dockerComposeDbContext.SaveChangesAsync();
            
            return product;
        }

        [HttpPut("{productId}")]
        public async Task<Product> Put(int productId, ProductDto productDto)
        {
            var product = await _dockerComposeDbContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Couldn't find the product");
            }
            product.Name = productDto.Name;
            product.Weight = productDto.Weight;
            product.UpdateDate = DateTime.Now;
            _dockerComposeDbContext.Update(product);
         
            await _dockerComposeDbContext.SaveChangesAsync();
            
            return product;
        }

        [HttpDelete("{productId}")]
        public async Task<Product> Delete(int productId)
        {
            var product = await _dockerComposeDbContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Couldn't find the product");
            }
            _dockerComposeDbContext.Remove(product);
            await _dockerComposeDbContext.SaveChangesAsync();
            return product;
        }
    }
}
