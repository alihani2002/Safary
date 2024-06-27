using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories;
using Service.Abstractions;

namespace Safary.Repository
{
	public class TourGuideRepository : BaseRepository<ApplicationUser>, ITourGuideRepository
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ITourGuideRepository _tourGuideRepository;
		private UserManager<ApplicationUser> _userManager;
        public TourGuideRepository(ApplicationDbContext context, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(context)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
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

		public async Task<ApplicationUser?> ToggleUserStatusAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user is null) return null;

			user.IsDeleted = !user.IsDeleted;

			await _userManager.UpdateAsync(user);

			if (user.IsDeleted)
				await _userManager.UpdateSecurityStampAsync(user);

			return user;

		}
        public async Task<ApplicationUser?> ToggleUserAcceptedStatusAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return null;

            user.AdminAccepted = !user.AdminAccepted;

            await _userManager.UpdateAsync(user);

            if (user.AdminAccepted)
                await _userManager.UpdateSecurityStampAsync(user);

            return user;

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
