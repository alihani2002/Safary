using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace Safary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public PlaceController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetPlaces()
        {
            var Places = await _unitOfWork.Places.GetAll();
            var dto = _mapper.Map<IEnumerable<PlaceDTO>>(Places);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPlaceById(int id)
        {


            var Place = await _unitOfWork.Places.GetById(id);

            if (Place is null)
                return NotFound();

            return Ok(Place);
        }
        [HttpPost]
        public async Task<IActionResult> AddPlace(PlacePostDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Place = _mapper.Map<Place>(model);

            await _unitOfWork.Places.Add(Place);

            _unitOfWork.Complete();
            return Ok(Place);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(int id, [FromForm] PlacePostDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingPlace = await _unitOfWork.Places.GetById(id);

            if (existingPlace == null)
                return NotFound();

            _mapper.Map(dto, existingPlace);

            _unitOfWork.Places.Update(existingPlace);
            _unitOfWork.Complete();

            return Ok(existingPlace);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var existingPlace = await _unitOfWork.Places.GetById(id);

            if (existingPlace == null)
                return NotFound();

            _unitOfWork.Places.Remove(existingPlace);
            _unitOfWork.Complete();

            return Ok(existingPlace);
        }

    }
}
