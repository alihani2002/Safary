using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using Persistence.Repositories;
using Shared.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourDayController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public TourDayController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetTourDays()
        {
            var TourDays = await _unitOfWork.TourDays.FindAll(s => s.Id > 0, include: s => s.Include(x => x.Places));
            var dto = _mapper.Map<IEnumerable<TourDayDTO>>(TourDays);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTourDayById(int id)
        {
            var TourDay = await _unitOfWork.TourDays.GetById(id);

            if (TourDay is null)
                return NotFound();

            return Ok(TourDay);
        }

        [HttpPost]
        public async Task<IActionResult> AddTourDay(TourDayPostDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var TourDay = _mapper.Map<TourDay>(model);

            await _unitOfWork.TourDays.Add(TourDay);
            _unitOfWork.Complete();
            return Ok(TourDay);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTourDay(int id, [FromForm] TourDayPostDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTourDay = await _unitOfWork.TourDays.GetById(id);

            if (existingTourDay == null)
                return NotFound();

            _mapper.Map(dto, existingTourDay);

            _unitOfWork.TourDays.Update(existingTourDay);
            _unitOfWork.Complete();

            return Ok(existingTourDay);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourDay(int id)
        {
            var existingTourDay = await _unitOfWork.TourDays.GetById(id);

            if (existingTourDay == null)
                return NotFound();

            _unitOfWork.TourDays.Remove(existingTourDay);
            _unitOfWork.Complete();

            return Ok(existingTourDay);
        }
    }
}
