using GmailTestApp.Model;
using Microsoft.EntityFrameworkCore;

namespace GmailTestApp.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Label> Labels { get; set; }
    }
}
