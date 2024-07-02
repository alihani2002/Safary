using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace Safary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TouristController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Tourists
        [HttpGet("GetAllTourist")]
        public async Task<ActionResult> GetTourist()
        {
            var users = await _unitOfWork.ApplicationUsers.FindAll(r => r.CvUrl == null, 0);
            var dto = _mapper.Map<IEnumerable<TouristDto>>(users);
            return Ok(dto);
        }

       
        // GET: api/Tourists/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTouristById(string id)
        {
            var users = await _unitOfWork.ApplicationUsers.Find(r => r.Id == id);
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var touristDto = _mapper.Map<TouristDto>(users);
            return Ok(touristDto);
        }

        [HttpGet("Email/{email}")]
        public async Task<ActionResult> GetTouristByEmail(string email)
        {
            if (!ModelState.IsValid)
                return NotFound();

            var tourist = await _unitOfWork.ApplicationUsers.Find(r => r.Email == email);

            if (tourist is null)
                return NotFound(ModelState);

            var tourgiudeDto = _mapper.Map<TouristDto>(tourist);
            return Ok(tourgiudeDto);
        }

        // POST: api/Tourists
        [HttpPost]
        public async Task<ActionResult<TouristDto>> CreateTourist([FromForm] TouristDto touristDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tourist = _mapper.Map<ApplicationUser>(touristDto);
            await _unitOfWork.ApplicationUsers.Add(tourist);
             _unitOfWork.Complete();

            var createdTouristDto = _mapper.Map<TouristDto>(tourist);
            return Ok(createdTouristDto);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTouristById(string id, [FromForm] TouristDto touristDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTourist = await _unitOfWork.ApplicationUsers.Find(r => r.Id == id);
            if (existingTourist == null || existingTourist.Id == null)
            {
                return NotFound();
            }

            // Ensure the email from the route is set in the DTO
            touristDto.Id = id;

            _mapper.Map(touristDto, existingTourist);
            _unitOfWork.ApplicationUsers.Update(existingTourist);
            _unitOfWork.Complete();
            return Ok(existingTourist);
        }

        // DELETE: api/Tourists/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTouristById(string id)
        {

            var tourist = await _unitOfWork.ApplicationUsers.Find(r => r.Id == id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (tourist == null || tourist.Id == null)
            {
                return NotFound();
            }

            _unitOfWork.ApplicationUsers.Remove(tourist);
            _unitOfWork.Complete();
            return Ok(tourist);
        }

    }
}
