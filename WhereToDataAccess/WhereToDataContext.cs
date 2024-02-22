using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToDataAccess.Enums;

namespace WhereToDataAccess
{
    public class WhereToDataContext : DbContext
    {
        #region DbSets

        public DbSet<User> Users { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<UserTour> UserTours { get; set; }
        public DbSet<TourCity> TourCities { get; set; }
        public DbSet<TourFeedback> TourFeedbacks { get; set; }
        public DbSet<BlobAttachments_feedbackphotos> BlobAttachments { get; set; }

        #endregion

        public WhereToDataContext(DbContextOptions<WhereToDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TourCity>()
                .HasKey(tc => new { tc.TourId, tc.CityId });

            modelBuilder.Entity<TourCity>()
                .HasOne<Tour>(tc => tc.Tour)
                .WithMany(t => t.TourCities)
                .HasForeignKey(tc => tc.TourId);

            modelBuilder.Entity<TourCity>()
                .HasOne<City>(tc => tc.City)
                .WithMany(c => c.TourCities)
                .HasForeignKey(tc => tc.CityId);

            modelBuilder.Entity<UserTour>()
                .HasKey(ut => new { ut.UserId, ut.TourId });

            modelBuilder.Entity<UserTour>()
                .HasOne<User>(ut => ut.User)
                .WithMany(u => u.UserTours)
                .HasForeignKey(ut => ut.UserId);

            modelBuilder.Entity<UserTour>()
                .HasOne<Tour>(ut => ut.Tour)
                .WithMany(t => t.UserTours)
                .HasForeignKey(ut => ut.TourId);

            modelBuilder.Entity<UserTour>()
                .Property(ut=>ut.Status)
                .HasConversion(new EnumToStringConverter<UserTourStatus>());

            modelBuilder.Entity<Tour>()
                .Property(t => t.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<BlobAttachments_feedbackphotos>()
                .HasOne<TourFeedback>(ba => ba.TourFeedback)
                .WithMany(tf => tf.FeedbackPhotos)
                .HasForeignKey(ba => ba.TourFeedbackId);

            modelBuilder.Entity<TourFeedback>()
                .HasOne<User>(tf=>tf.User)
                .WithMany(u=>u.TourFeedbacks)
                .HasForeignKey(tf=>tf.UserId);

            modelBuilder.Entity<TourFeedback>()
                .HasOne<Tour>(tf => tf.Tour)
                .WithMany(t => t.TourFeedbacks)
                .HasForeignKey(tf => tf.TourId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
