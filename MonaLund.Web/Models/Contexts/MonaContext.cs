using Microsoft.EntityFrameworkCore;

namespace MonaLund.Web.Models.Contexts
{
    public class MonaContext : DbContext
    {
        public DbSet<CategoryModel> Categories { get; set; }
        public MonaContext(DbContextOptions<MonaContext> options) : base(options)
        {
         
        }
    }
}
