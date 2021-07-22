using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class DBContext:IdentityDbContext<User>
    {
        public DBContext(DbContextOptions<DBContext>options):base(options)
        {
        }
        public DbSet<Friends> Friends { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Friends>()
                .HasOne(p => p.User1)
                .WithMany(t => t.CurrentUser)
                .HasForeignKey(p => p.User1Id);
            modelBuilder.Entity<Friends>()
                .HasOne(p => p.User2)
                .WithMany(t => t.Friends)
                .HasForeignKey(p => p.User2Id);
        }
    }
}
