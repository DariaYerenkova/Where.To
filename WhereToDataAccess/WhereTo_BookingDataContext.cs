using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.WhereTo_BookingEntities;

namespace WhereToDataAccess
{
    public class WhereTo_BookingDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserFlight> UserFlights { get; set; }
        public DbSet<UserHotel> UserHotels { get; set; }

        public WhereTo_BookingDataContext(DbContextOptions<WhereTo_BookingDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserFlights)
                .WithOne(uf => uf.User)
                .HasForeignKey(uf => uf.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserHotels)
                .WithOne(uh => uh.User)
                .HasForeignKey(uh => uh.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
