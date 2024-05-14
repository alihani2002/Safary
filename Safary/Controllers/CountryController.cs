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
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CountryController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetCountrys()
        {
            var Countrys = await _unitOfWork.Countries.FindAll(s => s.Id > 0, include: s => s.Include(x => x.Cities)!);
            var dto = _mapper.Map<IEnumerable<CountryDTO>>(Countrys);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCountryById(int id)
        {


            var Country = await _unitOfWork.Countries.GetById(id);

            if (Country is null)
                return NotFound();

            return Ok(Country);
        }
        [HttpPost]
        public async Task<IActionResult> AddCountry(CountryPostDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var country = _mapper.Map<Country>(model);

            await _unitOfWork.Countries.Add(country);

            _unitOfWork.Complete();
            return Ok(country);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromForm] CountryPostDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCountry = await _unitOfWork.Countries.GetById(id);

            if (existingCountry == null)
                return NotFound();

            _mapper.Map(dto, existingCountry);

            _unitOfWork.Countries.Update(existingCountry);
            _unitOfWork.Complete();

            return Ok(existingCountry);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var existingCountry = await _unitOfWork.Countries.GetById(id);

            if (existingCountry == null)
                return NotFound();

            _unitOfWork.Countries.Remove(existingCountry);
            _unitOfWork.Complete();

            return Ok(existingCountry);
        }
    }
}
