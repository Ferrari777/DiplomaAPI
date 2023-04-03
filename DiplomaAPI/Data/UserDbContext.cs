using DiplomaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace DiplomaAPI.Data
{
    public class UserDbContext : DbContext
    {
        //Options of context for connecting to DB, send info etc
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        //Representation of DB to manipulate data from table User
        public DbSet<User> Users { get; set; }
        //Representation of DB to manipulate data from table DocFile
        public DbSet<DocFile> DocFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocFile>();
        }
    }
}
