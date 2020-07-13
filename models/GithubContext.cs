using Microsoft.EntityFrameworkCore;


namespace TodoApi.Models
{
    public class GithubContext : DbContext
    {
        public GithubContext(DbContextOptions<GithubContext> options) : base(options) { }

        public DbSet<GithubItem> GithubItems { get; set; }
    }
}