using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Safary.Repository;
using Service.Abstractions;
using Shared.DTOs;
using Sieve.Models;
using Sieve.Services;
using System.Security.Claims;

namespace Safary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourGuidesController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITourGuideRepository _tourGuideRepository;
        private readonly ISieveProcessor _sieveProcessor;


        public TourGuidesController(IUnitOfWork unitOfWork, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ITourGuideRepository tourGuideRepository,
             ISieveProcessor sieveProcessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _tourGuideRepository = tourGuideRepository;
            _sieveProcessor = sieveProcessor;
        }

        // GET: api/Tourguide
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllTourGiudes()
        {
            var tourGuides = await _unitOfWork.ApplicationUsers.FindAll(r => r.CvUrl != null, 0);
            var dto = _mapper.Map<IEnumerable<CardTourGuideDTO>>(tourGuides);
            return Ok(dto);
        }

        [HttpGet("TourGuideTableById")]
        public async Task<IActionResult> TourGuidesSelectedById(string id)
        {
            var tourGuides = await _unitOfWork.SelectedTourGuides.FindAll(t => t.TourguideId == id && t.IsConfirmed == true, 0);
            
            return Ok(_mapper.Map<IEnumerable<TourGuideSelectedTableDTO>>(tourGuides));

        }

        [HttpGet("GetFilterdAndSorted")]
        public async Task<IActionResult> GetFilteredAndSorted([FromQuery] SieveModel sieveModel)
        {
            var tourGuides = _unitOfWork.ApplicationUsers
                .FilterFindAll(r => r.CvUrl != null);

            // Apply Sieve to the queryable collection
            var filteredSortedTourGuides = _sieveProcessor.Apply(sieveModel, tourGuides);
            var dto = _mapper.Map<IEnumerable<CardTourGuideDTO>>(filteredSortedTourGuides);
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
                TouristName = review.Tourist?.UserName,
            }).ToList();

            double averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0.0;

            // Set the ReviewsNumber property
            tourGuide.ReviewsNumber = reviews.Count;

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


        // POST: api/Tourguide
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

        // GET: api/Tourguide/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTourguideById(string id)
        {
			if (!ModelState.IsValid)
				return NotFound();

			var tourgiude = await _unitOfWork.ApplicationUsers.Find(r => r.Id == id);
            
            if (tourgiude is null)
                return NotFound(ModelState);

            var tourgiudeDto = _mapper.Map<TourgiudeDto>(tourgiude);
            return Ok(tourgiudeDto);
        }

        // PUT: api/Tourguide/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTourGuideById(string id, TourgiudeDto TourgiudeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tourguide = await _unitOfWork.ApplicationUsers.Find(r => r.Id == id);

            if (tourguide is null || tourguide.Id is null)
            {
                return NotFound();
            }

            _mapper.Map(TourgiudeDto, tourguide);
            _unitOfWork.ApplicationUsers.Update(tourguide);
            _unitOfWork.Complete();
            return Ok(tourguide);
        }

        // DELETE: api/Tourguide/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTourguide(string id)
        {
            var tourguide = await _unitOfWork.ApplicationUsers.Find(r => r.Email == id);
            if (tourguide == null || tourguide.Id == id)
            {
                return NotFound();
            }

            _unitOfWork.ApplicationUsers.Remove(tourguide);
            _unitOfWork.Complete();
            return Ok(tourguide);
        }

        [HttpPost("ToggleStatus/{id}")]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var user = await _tourGuideRepository.ToggleUserStatusAsync(id);

            return user is null ? NotFound() : Ok(user);
        }
        [HttpPost("ToggleStatusAccept/{id}")]
        public async Task<IActionResult> ToggleStatusAccept(string id)
        {
            var user = await _tourGuideRepository.ToggleUserAcceptedStatusAsync(id);

            return user is null ? NotFound() : Ok(user);
        }
    }
}


