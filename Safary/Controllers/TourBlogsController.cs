using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Abstractions;
using Shared.DTOs;
using Sieve.Models;
using Sieve.Services;
using System.Security.Claims;

namespace Safary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourBlogsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;



		public TourBlogsController(IMapper mapper, IUnitOfWork unitOfWork, ISieveProcessor sieveProcessor)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_sieveProcessor = sieveProcessor;
		}
		[HttpGet("GetFilterdAndSorted")]
        public async Task<IActionResult> GetFilterdAndSorted([FromQuery] SieveModel sieveModel)
        {
            var tours = _unitOfWork.Tours.FilterFindAll(s => s.Id > 0);

            // Apply Sieve to the queryable collection
            var filteredSortedPagedProducts = _sieveProcessor.Apply(sieveModel, tours);
            var dto = _mapper.Map<IEnumerable<TourDTO>>(filteredSortedPagedProducts);
            return Ok(dto);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetTours()
        {
            var Tours = await _unitOfWork.Tours.FindAll(s => s.Id > 0,0);
            var dto = _mapper.Map<IEnumerable<TourDTO>>(Tours);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTourById(int id)
        {
            var Tour = await _unitOfWork.Tours.GetById(id);

            if (Tour is null)
                return NotFound();

            return Ok(Tour);
        }

        [HttpPost]
        public async Task<IActionResult> AddTour(TourPostDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tour = _mapper.Map<TourBlog>(model);

            await _unitOfWork.TourBlogs.Add(tour);
            _unitOfWork.Complete();
            return Ok(tour);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTour(int id, [FromForm] TourPostDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTour = await _unitOfWork.Tours.GetById(id);

            if (existingTour == null)
                return NotFound();

            _mapper.Map(dto, existingTour);

            _unitOfWork.Tours.Update(existingTour);
            _unitOfWork.Complete();

            return Ok(existingTour);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTour(int id)
        {
            var existingTour = await _unitOfWork.Tours.GetById(id);

            if (existingTour == null)
                return NotFound();

            _unitOfWork.Tours.Remove(existingTour);
            _unitOfWork.Complete();

            return Ok(existingTour);
        }

		

	}


}
