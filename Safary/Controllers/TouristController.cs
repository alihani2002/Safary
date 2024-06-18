using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        [HttpGet("{email} GetTouristByEmail")]
        public async Task<ActionResult> GetTouristById(string email)
        {
            var users = await _unitOfWork.ApplicationUsers.Find(r => r.Email == email);
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var touristDto = _mapper.Map<TouristDto>(users);
            return Ok(touristDto);
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

        // PUT: api/Tourists/{id}
        [HttpPut("{email}")]
        public async Task<ActionResult> UpdateTourist([FromForm] string email, TouristDto touristDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingTourist = await _unitOfWork.ApplicationUsers.Find(r => r.Email == email);
            if (existingTourist == null || existingTourist.Email == null)
            {
                return NotFound();
            }

            _mapper.Map(touristDto, existingTourist);
            _unitOfWork.ApplicationUsers.Update(existingTourist);
            _unitOfWork.Complete();
            return Ok(existingTourist);
        }

        // DELETE: api/Tourists/{id}
        [HttpDelete("{email}")]
        public async Task<ActionResult> DeleteTourist(string email)
        {
           
            var tourist = await _unitOfWork.ApplicationUsers.Find(r => r.Email == email);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (tourist == null || tourist.Email == null)
            {
                return NotFound();
            }

            _unitOfWork.ApplicationUsers.Remove(tourist);
            _unitOfWork.Complete();
            return Ok(tourist);
        }
    }
}
