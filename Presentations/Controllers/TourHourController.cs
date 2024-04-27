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
        private readonly IBaseRepository<TourHour> _baseRepository;
        private readonly IMapper _mapper;


        public TourHourController(IBaseRepository<TourHour> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<TourHour>> GetTourHours()
        {
            try
            {
                var TourHours = _baseRepository.GetAll();
                return Ok(TourHours);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<TourHour> GetTourHour(int id)
        {
            try
            {
                var TourHour = _baseRepository.GetById(id);
                if (TourHour == null)
                {
                    return NotFound();
                }
                return Ok(TourHour);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<TourHour> PostTourHour(TourHourPostDTO dto)
        {
            try
            {
                if (dto != null)
                {
                    var TourHour = _mapper.Map<TourHour>(dto);
                    var createdTourHour = _baseRepository.Add(TourHour);
                    return CreatedAtAction(nameof(GetTourHour), new { id = createdTourHour.Id }, createdTourHour);
                }
                else { return BadRequest("Somethingwent wrong"); }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutTourHour(int id, TourHourDTO dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest();
                }
                if (dto != null)
                {
                    var TourHour = _mapper.Map<TourHour>(dto);
                    _baseRepository.Update(TourHour);
                    return Ok();
                }
                else { return BadRequest("Somethingwent wrong"); }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTourHour(int id)
        {
            try
            {
                _baseRepository.Remove(_baseRepository.GetById(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
