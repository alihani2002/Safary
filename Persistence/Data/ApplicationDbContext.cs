using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Xml;

namespace Persistence.Data
{
	public class ApplicationDbContext:  IdentityDbContext<ApplicationUser>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<TourReview> TourReviews { get; set; }
        public DbSet<TourGuideReview> TourGuideReviews { get; set; }
        public DbSet<SelectedTourGuide> SelectedTourGuides { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourImage> TourImages { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<SelectedTourGuide>()
            .HasKey(st => st.Id);

            modelBuilder.Entity<SelectedTourGuide>()
                .HasOne(st => st.Tourist)
                .WithMany()
                .HasForeignKey(st => st.TouristId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SelectedTourGuide>()
                .HasOne(st => st.Tourguide)
                .WithMany()
                .HasForeignKey(st => st.TourguideId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SelectedTourGuide>()
            .HasOne(st => st.Tour)
            .WithMany(t => t.SelectedTourGuides)
            .HasForeignKey(st => st.TourName)
            .HasPrincipalKey(t => t.Name);

            

            modelBuilder.Entity<Tour>()
            .Property(t => t.Id)
            .UseIdentityColumn();



            modelBuilder.Entity<SelectedTourGuide>()
		   .HasIndex(st => new { st.TouristId, st.TourguideId, st.SelectedDate })
		   .IsUnique(); // Ensures a tourist can select only one tour time per day

			base.OnModelCreating(modelBuilder);

        }
    }
}
