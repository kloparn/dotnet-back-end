using Microsoft.EntityFrameworkCore;


namespace TodoApi.Models
{
    public class InformationContext : DbContext
    {
        public InformationContext(DbContextOptions<InformationContext> options) : base(options) { }

        public DbSet<InformationItem> InformationItems { get; set; }
    }
}