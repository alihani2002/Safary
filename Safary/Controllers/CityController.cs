using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;
using Sieve.Models;
using Sieve.Services;

namespace Safary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CityController(IMapper mapper, IUnitOfWork unitOfWork, ISieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _sieveProcessor = sieveProcessor;
        }
        [HttpGet("GetFilterdAndSorted")]
        public async Task<IActionResult> GetFilterdAndSorted([FromQuery] SieveModel sieveModel)
        {
            var cities = _unitOfWork.Cities.FilterFindAll(s => s.Id > 0, include: s => s.Include(x => x.Places));

            // Apply Sieve to the queryable collection
            var filteredSortedPagedProducts = _sieveProcessor.Apply(sieveModel, cities);
            var dto = _mapper.Map<IEnumerable<CityDTO>>(filteredSortedPagedProducts);
            return Ok(dto);
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult> GetCities()
        {
            var cities = await _unitOfWork.Cities.FindAll(s => s.Id > 0, include: s => s.Include(x => x.Places));
            var dto = _mapper.Map<IEnumerable<CityDTO>>(cities);
            return Ok(dto);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetCityById(int id)
        {


            var City = await _unitOfWork.Cities.GetById(id);

            if (City is null)
                return NotFound();

            return Ok(City);
        }
        [HttpPost]
        public async Task<IActionResult> AddCity(CityPostDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var city = _mapper.Map<City>(model);

            await _unitOfWork.Cities.Add(city);

            _unitOfWork.Complete();
            return Ok(city);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromForm] CityPostDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCity = await _unitOfWork.Cities.GetById(id);

            if (existingCity == null)
                return NotFound();

            _mapper.Map(dto, existingCity);

            _unitOfWork.Cities.Update(existingCity);
            _unitOfWork.Complete();

            return Ok(existingCity);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var existingCity = await _unitOfWork.Cities.GetById(id);

            if (existingCity == null)
                return NotFound();

            _unitOfWork.Cities.Remove(existingCity);
            _unitOfWork.Complete();

            return Ok(existingCity);
        }

    }
}
