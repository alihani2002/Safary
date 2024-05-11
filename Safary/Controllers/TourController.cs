using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;

namespace Safary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public TourController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetTours()
        {
            var Tours = await _unitOfWork.Tours.FindAll(s => s.Id > 0, include: s => s.Include(x => x.Places));
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
        public async Task<IActionResult> AddTour(TourDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tour = _mapper.Map<Tour>(model);

            await _unitOfWork.Tours.Add(tour);
            _unitOfWork.Complete();
            return Ok(tour);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTour(int id, TourDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTour = await _unitOfWork.Tours.GetById(id);

            if (existingTour == null)
                return NotFound();

            existingTour = _mapper.Map<Tour>(model);

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
