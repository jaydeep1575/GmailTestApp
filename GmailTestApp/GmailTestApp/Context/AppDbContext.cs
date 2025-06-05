using GmailTestApp.Model;
using Microsoft.EntityFrameworkCore;

namespace GmailTestApp.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Label> Labels { get; set; }
        public DbSet<Gmail> Gmails { get; set; }
        public DbSet<GmailLabelMap> GmailLabelMap { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GmailLabelMap>()
                .HasKey(gl => new { gl.GmailId, gl.LabelId });

            modelBuilder.Entity<GmailLabelMap>()
                .HasOne(gl => gl.Gmail)
                .WithMany(e => e.GmailLabelMaps)
                .HasForeignKey(gl => gl.GmailId);

            modelBuilder.Entity<GmailLabelMap>()
                .HasOne(gl => gl.Label)
                .WithMany(l => l.GmailLabelMaps)
                .HasForeignKey(gl => gl.LabelId);
        }
    }
}
