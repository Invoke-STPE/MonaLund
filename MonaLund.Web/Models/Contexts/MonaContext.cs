using Microsoft.EntityFrameworkCore;

namespace MonaLund.Web.Models.Contexts
{
    public class MonaContext : DbContext
    {
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<SubCategoryModel> SubCategories { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public MonaContext(DbContextOptions<MonaContext> options) : base(options)
        {
         
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryModel>()
                .HasMany(p => p.SubCategories)
                .WithOne(c => c.Category);
            modelBuilder.Entity<SubCategoryModel>()
                .HasMany(p => p.Products)
                .WithOne(s => s.SubCategory);
        }
    }
}
