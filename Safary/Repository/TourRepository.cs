using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories;
using Service.Abstractions;

namespace Safary.Repository
{
	public class TourRepository:BaseRepository<TourHour>, ITourRepository
	{
		public TourRepository(ApplicationDbContext context) : base(context)
		{

		}


		public async Task<bool> SelectTourAsync(string userId, string tourName)
		{
			var tour = await _context.tourHours.FirstOrDefaultAsync(t => t.Name == tourName);

			if (tour == null)
			{
				// Handle if the tour with the specified name does not exist
				return false;
			}

			var selectedTourGuide = await _context.SelectedTourGuides
				.FirstOrDefaultAsync(st => st.TouristId == userId);

			if (selectedTourGuide == null)
			{
				// Handle if no existing selected tour guide is found
				return false;
			}

			selectedTourGuide.TourName = tourName;

			await _context.SaveChangesAsync();

			return true;
		}
	}
}
