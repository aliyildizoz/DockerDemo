using Microsoft.EntityFrameworkCore;

namespace DockerDemoWebApi.DockerComposeDbContext
{
    public class DockerComposeDemoDbContext : DbContext
    {
        public DockerComposeDemoDbContext(DbContextOptions<DockerComposeDemoDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
