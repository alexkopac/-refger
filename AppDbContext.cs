using Microsoft.EntityFrameworkCore;
using ProjectTestingNote.Models;

namespace ProjectTestingNote.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Note> Notes => Set<Note>();
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Note
            modelBuilder.Entity<Note>();

            base.OnModelCreating(modelBuilder);
        }
        


    }
}
