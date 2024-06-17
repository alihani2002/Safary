using AutoMapper;
using Domain.Consts;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using System.Globalization;

namespace Safary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourGuidesController : ControllerBase
    {

        //public IActionResult GetAll(string duration)
        //{
        //	// !(specificStartDate <= user.EndDate && specificEndDate >= user.StartDate)

        //	if (!string.IsNullOrEmpty(duration))
        //	{
        //		if (!DateTime.TryParse(duration.Split(separator: " - ")[0], out DateTime from))
        //		{
        //			ModelState.AddModelError("Duration", Errors.InvalidStartDate);
        //			return BadRequest(ModelState);
        //		}

        //		if (!DateTime.TryParse(duration.Split(" - ")[1], out DateTime to))
        //		{
        //			ModelState.AddModelError("Duration", Errors.InvalidEndDate);
        //			return BadRequest(ModelState);
        //		}

        //	}

        //}

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TourGuidesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Tourists
        [HttpGet("GetAllTourGiudes")]
        public async Task<ActionResult> GetTourist()
        {
            var users = await _unitOfWork.ApplicationUsers.FindAll(r => r.CvUrl != null, 0);
            var dto = _mapper.Map<IEnumerable<TourgiudeDto>>(users);
            return Ok(dto);
        }

       

        // POST: api/Tourists
        [HttpPost]
        public async Task<ActionResult<TouristDto>> CreateTourist(TourgiudeDto tourgiudeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Tourgiude = _mapper.Map<ApplicationUser>(tourgiudeDto);
            await _unitOfWork.ApplicationUsers.Add(Tourgiude);
            _unitOfWork.Complete();

            var createdTouristDto = _mapper.Map<TouristDto>(Tourgiude);
            return CreatedAtAction(nameof(GetTourist), new { id = Tourgiude.Id }, createdTouristDto);
        }

        //// GET: api/Tourists/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TouristDto>> GetTourist(int id)
        //{
        //    var tourist = await _unitOfWork.ApplicationUsers.GetById(id);
        //    if (!ModelState.IsValid)
        //    {
        //        return NotFound();
        //    }
        //    var touristDto = _mapper.Map<TouristDto>(tourist);
        //    return Ok(touristDto);
        //}

        //    // PUT: api/Tourists/{id}
        //    [HttpPut("{id}")]
        //    public async Task<ActionResult> UpdateTourist(int id, TouristDto touristDto)
        //    {

        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var existingTourist = await _unitOfWork.ApplicationUsers.GetById(id);
        //        if (existingTourist == null || existingTourist.TouristId == null)
        //        {
        //            return NotFound();
        //        }

        //        _mapper.Map(touristDto, existingTourist);
        //        _unitOfWork.ApplicationUsers.Update(existingTourist);
        //        _unitOfWork.Complete();
        //        return Ok(existingTourist);
        //    }

        //    // DELETE: api/Tourists/{id}
        //    [HttpDelete("{id}")]
        //    public async Task<ActionResult> DeleteTourist(int id)
        //    {
        //        var tourist = await _unitOfWork.ApplicationUsers.GetById(id);
        //        if (tourist == null || tourist.TouristId == null)
        //        {
        //            return NotFound();
        //        }

        //        _unitOfWork.ApplicationUsers.Remove(tourist);
        //        _unitOfWork.Complete();
        //        return Ok(tourist);
        //    }
        //}





    }
}

