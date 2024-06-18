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
        [HttpGet("")]
        public async Task<ActionResult> GetAllTourGiudes()
        {
            var users = await _unitOfWork.ApplicationUsers.FindAll(r => r.CvUrl != null, 0);
            var dto = _mapper.Map<IEnumerable<TourgiudeDto>>(users);
            return Ok(dto);
        }

        // POST: api/Tourists
        [HttpPost]
        public async Task<ActionResult> CreateTourguide([FromForm]TourgiudeDto tourgiudeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Tourgiude = _mapper.Map<ApplicationUser>(tourgiudeDto);
            await _unitOfWork.ApplicationUsers.Add(Tourgiude);
            _unitOfWork.Complete();

            var createdTourgiude = _mapper.Map<TourgiudeDto>(Tourgiude);
            return Ok(createdTourgiude);
        }

        // GET: api/Tourists/{id}
        [HttpGet("{email}")]
        public async Task<ActionResult> GetTourguideByEmail(string email)
        {
            var tourgiude = await _unitOfWork.ApplicationUsers.Find(r => r.Email == email);
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var touristDto = _mapper.Map<TouristDto>(tourgiude);
            return Ok(touristDto);
        }

        // PUT: api/Tourists/{id}
        [HttpPut("{email}")]
        public async Task<ActionResult> UpdateTourGuide(string email, TourgiudeDto TourgiudeDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tourguide = await _unitOfWork.ApplicationUsers.Find(r => r.Email == email);

            if (tourguide == null || tourguide.Email == null)
            {
                return NotFound();
            }

            _mapper.Map(TourgiudeDto, tourguide);
            _unitOfWork.ApplicationUsers.Update(tourguide);
            _unitOfWork.Complete();
            return Ok(tourguide);
        }

        // DELETE: api/Tourists/{id}
        [HttpDelete("{email}")]
        public async Task<ActionResult> DeleteTourguide(string email)
        {
            var tourguide = await _unitOfWork.ApplicationUsers.Find(r => r.Email == email);
            if (tourguide == null || tourguide.Email == email)
            {
                return NotFound();
            }

            _unitOfWork.ApplicationUsers.Remove(tourguide);
            _unitOfWork.Complete();
            return Ok(tourguide);
        }
    }
}


