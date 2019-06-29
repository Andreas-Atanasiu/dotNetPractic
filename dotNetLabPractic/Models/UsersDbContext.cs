using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabPractic.Models
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
            });

            builder.Entity<Packet>(entity => {
                entity.HasIndex(p => p.Code).IsUnique();
            });

            builder.Entity<PacketHistory>()
            .HasOne(p => p.Packet)
            .WithMany(h => h.PacketHistories)
            .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User>   Users   { get; set; }
        public DbSet<Packet> Packets { get; set; }
        public DbSet<PacketHistory> PacketHistories { get; set; }



    }
}
