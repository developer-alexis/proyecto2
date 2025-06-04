using Microsoft.EntityFrameworkCore;
using proyecto2.Models;

namespace proyecto2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Posts)
                .WithOne(p => p.Account)
                .HasForeignKey(p => p.AccountId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Replies)
                .WithOne(r => r.Post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
