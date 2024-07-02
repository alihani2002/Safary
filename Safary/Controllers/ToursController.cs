using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using Safary.Repository;
using Service.Abstractions;
using Shared.DTOs;
using Sieve.Models;
using Sieve.Services;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly ITourRepository _tourRepository;
        private readonly ITourService _tourService;
		private readonly IHttpContextAccessor _httpContextAccessor;


        public ToursController(IMapper mapper, IUnitOfWork unitOfWork, ISieveProcessor sieveProcessor, IHttpContextAccessor httpContextAccessor, ITourRepository tourRepository, ITourService tourService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _sieveProcessor = sieveProcessor;
            _httpContextAccessor = httpContextAccessor;
            _tourRepository = tourRepository;
            _tourService = tourService;
        }
        [HttpGet("GetFilterdAndSorted")]
        public async Task<IActionResult> GetFilterdAndSorted([FromQuery] SieveModel sieveModel)
        {
            var tours = _unitOfWork.Tours.FilterFindAll(s => s.Id > 0).Include(x => x.TourImages);

            // Apply Sieve to the queryable collection
            var filteredSortedPagedProducts = _sieveProcessor.Apply(sieveModel, tours);
            var dto = _mapper.Map<IEnumerable<TourHourDTO>>(filteredSortedPagedProducts);
            return Ok(dto);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetTours()
        {
            var tours = await _tourRepository.GetAllToursWithImages();

            var tourDtos = new List<TourHourDTO>();

            foreach (var tour in tours)
            {
                var reviews = await _unitOfWork.TourReviews.FindAll(r => r.TourName == tour.Name, 0);
                var averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0.0;

                var tourDto = _mapper.Map<TourHourDTO>(tour);
                tourDto.AverageRating = averageRating;

                tourDtos.Add(tourDto);
            }

            return Ok(tourDtos);
        }

        [HttpGet("GetTourDetails")]
        public async Task<ActionResult<TourDetailsDTO>> GetTourDetails(string name)
        {
            var tour = await _tourRepository.GetToursImagesWithName(name);

            if (tour == null)
                return NotFound($"Tour with ID '{name}' not found.");

            // Fetch reviews
            var reviews = await _unitOfWork.TourReviews.FindAll(r => r.TourName == name, 0);
            var reviewDtos = _mapper.Map<IEnumerable<TourReviewDetailsDTO>>(reviews);

            // Calculate average rating
            double averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0.0;

            var tourDto = _mapper.Map<TourDetailsDTO>(tour);
            tourDto.Reviews = reviewDtos.ToList();
            tourDto.AverageRating = averageRating; // Set the average rating
            tourDto.NumberOfReviews = reviews.Count();

            return Ok(tourDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddTour(CreateTourDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var TourHour = _mapper.Map<Tour>(model);

            await _unitOfWork.Tours.Add(TourHour);
            _unitOfWork.Complete();
            return Ok( $"Id :{TourHour.Id} ,\n" +
                       $"Name {TourHour.Name}\n," +
                       $"Location {TourHour.Location}\n," +
                       $"Description {TourHour.Description} " );
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name,  TourHourPostDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isUpdated = await _tourRepository.Update(name, dto);

            if (!isUpdated)
                return BadRequest(ModelState);

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteTourHour(string name)
        {
            var existingTourHour = await _unitOfWork.Tours.GetByName(name);

            if (existingTourHour == null)
                return NotFound();

            _unitOfWork.Tours.Remove(existingTourHour);
            _unitOfWork.Complete();

            return Ok(existingTourHour);
        }
		[HttpPost("SelectTourAndUpdateInSelectTourGuideTable")]
		[Authorize("UserPolicy")]
		public async Task<IActionResult> SelectTour([FromBody] string tourName)
		{
			if (string.IsNullOrEmpty(tourName))
				return BadRequest("TourName must be provided.");

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue("uid");

			if (userId == null)
				return Unauthorized("User not authorized.");

			try
			{
				var selected = await _tourRepository.SelectTourAsync(userId, tourName);

				if (selected)
				{
					return Ok($"Successfully selected tour '{tourName}'");
				}
				else
				{
					return NotFound($"Tour '{tourName}' not found or user has no permissions.");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Failed to select tour: {ex.Message}");
			}
		}

		[HttpPost("ConfirmTour")]
		[Authorize("UserPolicy")]
		public async Task<IActionResult> ConfirmTour()
		{
			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue("uid");

			if (userId == null)
				return Unauthorized("User not authorized.");

			try
			{
				var selected = await _tourRepository.ConfirmedTourAsync(userId);

				if (selected)
				{
					return Ok("Your Tour is confirmed");
				}
				else
				{
					return NotFound("Something went wrong!");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Failed to select tour: {ex.Message}");
			}
		}



		[HttpPost("AddTourImages")]
        public async Task<IActionResult> AddTourImages([FromBody] TourImagesDTO dto)
        {
            var tour = await _unitOfWork.Tours.Find(n => n.Name == dto.TourName);

            if (tour == null)
            {
                return NotFound("Tour not found.");
            }

            var tourImages = dto.ImageUrls.Select(url => new TourImage
            {
                TourName = dto.TourName,
                ImageUrl = url
            }).ToList();

            await _unitOfWork.TourImages.AddRange(tourImages);
            _unitOfWork.Complete();

            return Ok("Images successfully added to the tour.");
        }

        [HttpPost("ToggleStatus/{name}")]
        public async Task<IActionResult> ToggleStatus(string name)
        {
            var tour = await _tourService.ToggleStatus(name);

            return tour is null ? NotFound() : Ok(tour);
        }
    }
}
