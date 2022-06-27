using Microsoft.EntityFrameworkCore;

namespace MonaLund.Web.Models.Contexts
{
    public class MonaContext : DbContext
    {
        public MonaContext(DbContextOptions<MonaContext> options) : base(options)
        {

        }
    }
}
