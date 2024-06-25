using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Abstractions;
using Shared.DTOs;
using System.Security.Claims;

namespace Safary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourGuidesController : ControllerBase
    {

        //public IActionResult GetAll(string duration)
        //{
        //	// !(specificStartDate <= user.EndDate && specificEndDate >= user.StartDate)

        //	if (!string.IsNullOrEmpty(duration))
        //	{
        //		if (!DateTime.TryParse(duration.Split(separator: " - ")[0], out DateTime from))
        //		{
        //			ModelState.AddModelError("Duration", Errors.InvalidStartDate);
        //			return BadRequest(ModelState);
        //		}

        //		if (!DateTime.TryParse(duration.Split(" - ")[1], out DateTime to))
        //		{
        //			ModelState.AddModelError("Duration", Errors.InvalidEndDate);
        //			return BadRequest(ModelState);
        //		}

        //	}

        //}

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITourGuideRepository _tourGuideRepository;

		public TourGuidesController(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ITourGuideRepository tourGuideRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
			_tourGuideRepository = tourGuideRepository;
		}

		// GET: api/Tourists
		[HttpGet("GetAll")]
        public async Task<ActionResult> GetAllTourGiudes()
        {
            var tourGuides = await _unitOfWork.ApplicationUsers.FindAll(r => r.CvUrl != null, 0);
            var dto = _mapper.Map<IEnumerable<CardTourGuideDTO>>(tourGuides);
            return Ok(dto);
        }

        [HttpGet("GetDetails")]
        public async Task<ActionResult> Details(string id)
        {

			var tourGuide = await _unitOfWork.ApplicationUsers
	         .Find(g => g.Id == id);
			if (tourGuide is null) return NotFound();

			var reviews = await _unitOfWork.TourGuideReviews
				.FilterFindAll(r => r.TourGuideId == id, c => c.Include(x => x.Tourist))
				.ToListAsync();

			var reviewDtos = reviews.Select(review => new TourGuideReviewDetailsDTO
			{
				Rating = review.Rating,
				Comment = review.Comment,
				TouristName = review.Tourist?.UserName 
			}).ToList();

			double averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0.0;

			var dto = _mapper.Map<TourGuideDetailsDTO>(tourGuide);

			dto.Reviews = reviewDtos;
			dto.AverageRating = averageRating; 

			return Ok(dto);
		}

        [HttpPost("AddTourGuideSelected")]
        [Authorize("UserPolicy")]
        public async Task<IActionResult> TourGuideSelected([FromForm] TourGuideSelectedDTO dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue("uid")!;

            await _tourGuideRepository.AddSelectTourGuide(userId,dto.TourGuideId,dto.SelectedDate,dto.TimeToCast,dto.Adults);

            return Ok("Successfully save in db");
		}



        // POST: api/Tourists
        [HttpPost]
        public async Task<ActionResult> CreateTourguide([FromForm]TourgiudeDto tourgiudeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Tourgiude = _mapper.Map<ApplicationUser>(tourgiudeDto);
            await _unitOfWork.ApplicationUsers.Add(Tourgiude);
            _unitOfWork.Complete();

            var createdTourgiude = _mapper.Map<TourgiudeDto>(Tourgiude);
            return Ok(createdTourgiude);
        }

        // GET: api/Tourists/{id}
        [HttpGet("{email}")]
        public async Task<ActionResult> GetTourguideByEmail(string email)
        {
			if (!ModelState.IsValid)
				return NotFound();

			var tourgiude = await _unitOfWork.ApplicationUsers.Find(r => r.Email == email);
            
            if (tourgiude is null)
                return NotFound(ModelState);

            var tourgiudeDto = _mapper.Map<TourgiudeDto>(tourgiude);
            return Ok(tourgiudeDto);
        }

		// PUT: api/Tourists/{id}
		[HttpPut("{email}")]
        public async Task<ActionResult> UpdateTourGuide(string email, TourgiudeDto TourgiudeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tourguide = await _unitOfWork.ApplicationUsers.Find(r => r.Email == email);

            if (tourguide is null || tourguide.Email is null)
            {
                return NotFound();
            }

            _mapper.Map(TourgiudeDto, tourguide);
            _unitOfWork.ApplicationUsers.Update(tourguide);
            _unitOfWork.Complete();
            return Ok(tourguide);
        }

        // DELETE: api/Tourists/{id}
        [HttpDelete("{email}")]
        public async Task<ActionResult> DeleteTourguide(string email)
        {
            var tourguide = await _unitOfWork.ApplicationUsers.Find(r => r.Email == email);
            if (tourguide == null || tourguide.Email == email)
            {
                return NotFound();
            }

            _unitOfWork.ApplicationUsers.Remove(tourguide);
            _unitOfWork.Complete();
            return Ok(tourguide);
        }
    }
}


