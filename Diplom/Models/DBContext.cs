using Diplom.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Models
{
    public class DBContext:IdentityDbContext<MyUser>
    {
        public DBContext(DbContextOptions<DBContext>options):base(options)
        {
        }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Dialogs> Dialogs { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<MessageStatus> MessageStatus { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Photos> Photos { get; set; }
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
                .HasForeignKey(p => p.User2Id)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<MyUser>()
                .HasMany(c => c.Dialogs)
                .WithMany(s => s.Users)
                .UsingEntity(j => j.ToTable("users_to_dialogs"));
            modelBuilder.Entity<Messages>()
                .HasOne(p => p.Dialogs)
                .WithMany(c => c.Messages)
                .HasForeignKey(p=>p.DialogsId);
            modelBuilder.Entity<Messages>()
                .HasOne(p => p.User)
                .WithMany(c => c.Messages)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Messages>()
                .HasMany(p => p.MessageStatus)
                .WithOne(c => c.Messages)
                .HasForeignKey(p => p.MessagesId)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<MessageStatus>()
                .HasOne(p => p.User)
                .WithMany(c => c.MessageStatus)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MyUser>()
                .HasMany(x => x.Posts)
                .WithOne(c => c.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Posts>()
                .HasMany(x => x.Photos)
                .WithOne(c => c.Post)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Messages>()
                .HasMany(x => x.Photos)
                .WithOne(c => c.Message)
                .HasForeignKey(p => p.MessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
