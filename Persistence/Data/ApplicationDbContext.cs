using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Data
{
	public class ApplicationDbContext:  IdentityDbContext<ApplicationUser>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<TourHour> tourHours { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SelectedTourGuide> SelectedTourGuides { get; set; }

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
		   .HasIndex(st => new { st.TouristId, st.TourguideId, st.SelectedDate })
		   .IsUnique(); // Ensures a tourist can select only one tour time per day

			base.OnModelCreating(modelBuilder);

        }
    }
}
