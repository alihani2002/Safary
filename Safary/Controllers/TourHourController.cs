using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using Shared.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourHourController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public TourHourController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetTourHours()
        {
            var TourHours = await _unitOfWork.TourHours.FindAll(s => s.Id > 0, include: s => s.Include(x => x.Places));
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

        [HttpPost]
        public async Task<IActionResult> AddTourHour(TourHourDTO model)
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
        public async Task<IActionResult> UpdateTourHour(int id, TourHourDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTourHour = await _unitOfWork.TourHours.GetById(id);

            if (existingTourHour == null)
                return NotFound();

            existingTourHour = _mapper.Map<TourHour>(model);

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
