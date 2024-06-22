using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using Shared.DTOs;
using Sieve.Models;
using Sieve.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourHourController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;

        public TourHourController(IMapper mapper, IUnitOfWork unitOfWork, ISieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _sieveProcessor = sieveProcessor;
        }
        [HttpGet("GetFilterdAndSorted")]
        public async Task<IActionResult> GetFilterdAndSorted([FromQuery] SieveModel sieveModel)
        {
            var tourHours = _unitOfWork.TourHours.FilterFindAll(s => s.Id > 0);

            // Apply Sieve to the queryable collection
            var filteredSortedPagedProducts = _sieveProcessor.Apply(sieveModel, tourHours);
            var dto = _mapper.Map<IEnumerable<TourHourDTO>>(filteredSortedPagedProducts);
            return Ok(dto);
        }
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
    }
}
