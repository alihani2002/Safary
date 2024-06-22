using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories;
using Service.Abstractions;

namespace Safary.Repository
{
	public class TourGuideRepository : BaseRepository<ApplicationUser>, ITourGuideRepository
	{

		public TourGuideRepository(ApplicationDbContext context) : base(context)
		{

		}

		public async Task<bool> AddSelectTourGuide(string touristId, string tourGuideId, DateTime tourTime, string timeToCast,int adults)
		{
			// Ensure the tourist and tour guide exist
			var tourist = await _context.Set<ApplicationUser>().FindAsync(touristId);
			var tourGuide = await _context.Set<ApplicationUser>().FindAsync(tourGuideId);

			if (tourist == null || tourGuide == null)
			{
				throw new InvalidOperationException("Invalid tourist or tour guide.");
			}

			// Check if the tourist already has a select time for the same day
			var existingSelectTime = await _context.SelectedTourGuides
				.FirstOrDefaultAsync(st => st.TouristId == touristId && st.SelectedDate == tourTime);

			if (existingSelectTime != null)
			{
				throw new InvalidOperationException("The tourist already has a select time for this day.");
			}

			int hour = int.Parse(timeToCast.Split(':')[0]);
			int minute = int.Parse(timeToCast.Split(':')[1]);

			var timeSelected = new TimeOnly(hour, minute);

			var selectedTourGuide = new SelectedTourGuide
			{
				TouristId = touristId,
				TourguideId = tourGuideId,
				SelectedDate = tourTime,
				SelectedTime = timeSelected,
				Adults = adults
				
			};
			

			_context.SelectedTourGuides.Add(selectedTourGuide);
			await _context.SaveChangesAsync();

			return true;
		}



		//public IEnumerable<ApplicationUser> GetAll(DateTime from, DateTime to)
		//{
		//	throw new NotImplementedException();
		//}
		// !(specificStartDate <= user.EndDate && specificEndDate >= user.StartDate)

		//public IEnumerable<ApplicationUser> GetAll(DateTime from, DateTime to) =>
		//	 _context.Set<ApplicationUser>().Where(g => !(g.StartDate <= to && g.EndDate >= from))
		//		.ToList();



		//public IEnumerable<ApplicationUser> GetAll(DateTime from, DateTime to) =>
		//	 _context.Set<ApplicationUser>().Where(g => !(g.StartDate <= to && g.EndDate >= to) || !(g.StartDate <= from && g.EndDate >= from))
		//		.ToList();
	}
}
