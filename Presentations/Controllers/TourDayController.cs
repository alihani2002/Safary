using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourDayController : ControllerBase
    {
        private readonly IBaseRepository<TourDay> _baseRepository;

        public TourDayController(IBaseRepository<TourDay> baseRepository)
        {
            _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<TourDay>> GetTourDays()
        {
            try
            {
                var tourDays = _baseRepository.GetAll();
                return Ok(tourDays);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<TourDay> GetTourDay(int id)
        {
            try
            {
                var tourDay = _baseRepository.GetById(id);
                if (tourDay == null)
                {
                    return NotFound();
                }
                return Ok(tourDay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<TourDay> PostTourDay(TourDay tourDay)
        {
            try
            {
                var createdTourDay = _baseRepository.Add(tourDay);
                return CreatedAtAction(nameof(GetTourDay), new { id = createdTourDay.Id }, createdTourDay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutTourDay(int id, TourDay tourDay)
        {
            try
            {
                if (id != tourDay.Id)
                {
                    return BadRequest();
                }

                _baseRepository.Update(tourDay);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTourDay(int id)
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
