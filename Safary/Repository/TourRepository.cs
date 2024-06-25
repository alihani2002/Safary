using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories;
using Service.Abstractions;

namespace Safary.Repository
{
	public class TourRepository : BaseRepository<Tour>, ITourRepository
	{
		public TourRepository(ApplicationDbContext context) : base(context)
		{

		}

		public async Task<bool> ConfirmedTourAsync(string userId)
		{

			var selectedTourGuide = await _context.SelectedTourGuides
						.Where(stg => stg.TouristId == userId)
						.OrderByDescending(stg => stg.Id)
						.FirstOrDefaultAsync();

			if (selectedTourGuide == null)
			{
				// Handle if no existing selected tour guide is found
				return false;
			}

			selectedTourGuide.IsConfirmed = true;

			await _context.SaveChangesAsync();

			return true;
		}

       

        public async Task<IEnumerable<Tour>> GetAllToursWithImages()
		{
			return await _context.Tours.Include(t => t.TourImages).ToListAsync();
		}

        public Task<Tour> GetToursImagesWithName(string name)
        {
            return _context.Tours.Include(t => t.TourImages).FirstOrDefaultAsync(t => t.Name == name);

        }

        public async Task<bool> SelectTourAsync(string userId, string tourName)
		{
			var tour = await _context.Tours.FirstOrDefaultAsync(t => t.Name == tourName);

			if (tour == null)
			{
				// Handle if the tour with the specified name does not exist
				return false;
			}


			var selectedTourGuide = await _context.SelectedTourGuides
						.Where(stg => stg.TouristId == userId)
						.OrderByDescending(stg => stg.Id)
						.FirstOrDefaultAsync();

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
