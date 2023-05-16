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
        //Representation of DB to manipulate data from table UserRole
        public DbSet<UserRole> UserRoles { get; set; }
        //Representation of DB to manipulate data from table Agreed
        public DbSet<Agreed> Agreed { get; set; }
        //Representation of DB to manipulate data from table Approved
        public DbSet<Approved> Approved { get; set; }
        //Representation of DB to manipulate data from table Document
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(p => p.UserRole)
                .WithMany(m => m.Users)
                .HasForeignKey(k => k.UserRoleId);

            modelBuilder.Entity<DocFile>()
                .HasOne(p => p.User)
                .WithMany(m => m.DocFiles)
                .HasForeignKey(k => k.UserId);

            modelBuilder.Entity<DocFile>()
                .HasOne(p => p.Agreed)
                .WithOne(m => m.DocFile)
                .HasForeignKey<Agreed>(k => k.DocFileId);

            modelBuilder.Entity<DocFile>()
                .HasOne(p => p.Approved)
                .WithOne(m => m.DocFile)
                .HasForeignKey<Approved>(k => k.DocFileId);
        }
    }
}
