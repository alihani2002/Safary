using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;
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
    public class TourHoursController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly ITourRepository _tourRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;


		public TourHoursController(IMapper mapper, IUnitOfWork unitOfWork, ISieveProcessor sieveProcessor, IHttpContextAccessor httpContextAccessor, ITourRepository tourRepository)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_sieveProcessor = sieveProcessor;
			_httpContextAccessor = httpContextAccessor;
			_tourRepository = tourRepository;
		}
		//[HttpGet("GetFilterdAndSorted")]
  //      public async Task<IActionResult> GetFilterdAndSorted([FromQuery] SieveModel sieveModel)
  //      {
  //          var tours = _unitOfWork.Tours.FilterFindAll(s => s.Id > 0);

  //          // Apply Sieve to the queryable collection
  //          var filteredSortedPagedProducts = _sieveProcessor.Apply(sieveModel, tours);
  //          var dto = _mapper.Map<IEnumerable<TourHourDTO>>(filteredSortedPagedProducts);
  //          return Ok(dto);
  //      }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetTourHours()
        {
            var TourHours = await _unitOfWork.TourHours.GetAll();
            var dto = _mapper.Map<IEnumerable<TourHourDTO>>(TourHours);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTourHourById(int id)
        {
            var TourHour = await _unitOfWork.TourHours.GetById(id);

            if (TourHour is null)
                return NotFound();

            return Ok(TourHour);
        }

        //[HttpGet("TourGuideId")]
        //public async Task<IActionResult> ToursByTourGuideId(string email)
        //{
        //    var tours = await _unitOfWork.TourHours.FindAll(s => s.)
        //}

        [HttpPost]
        public async Task<IActionResult> AddTourHour(TourHourPostDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var TourHour = _mapper.Map<TourHour>(model);

            await _unitOfWork.TourHours.Add(TourHour);
            _unitOfWork.Complete();
            return Ok(TourHour);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTourHour(int id, [FromForm] TourHourPostDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTourHour = await _unitOfWork.TourHours.GetById(id);

            if (existingTourHour == null)
                return NotFound();

            _mapper.Map(dto, existingTourHour);

            _unitOfWork.TourHours.Update(existingTourHour);
            _unitOfWork.Complete();

            return Ok(existingTourHour);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourHour(int id)
        {
            var existingTourHour = await _unitOfWork.TourHours.GetById(id);

            if (existingTourHour == null)
                return NotFound();

            _unitOfWork.TourHours.Remove(existingTourHour);
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
	}
}
