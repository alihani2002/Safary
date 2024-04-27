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
    public class BlogController : ControllerBase
    {
        private readonly IBaseRepository<Blog> _baseRepository;
        private readonly IMapper _mapper;


        public BlogController(IBaseRepository<Blog> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Blog>> GetBlogs()
        {
            try
            {
                var Blogs = _baseRepository.GetAll();
                return Ok(Blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Blog> GetBlog(int id)
        {
            try
            {
                var Blog = _baseRepository.GetById(id);
                if (Blog == null)
                {
                    return NotFound();
                }
                return Ok(Blog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<Blog> PostBlog(BlogPostDTO dto)
        {
            try
            {
                if (dto != null)
                {
                    var Blog = _mapper.Map<Blog>(dto);
                    var createdBlog = _baseRepository.Add(Blog);
                    return CreatedAtAction(nameof(GetBlog), new { id = createdBlog.Id }, createdBlog);
                }
                else { return BadRequest("Somethingwent wrong"); }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutBlog(int id, BlogDTO dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest();
                }
                if (dto != null)
                {
                    var Blog = _mapper.Map<Blog>(dto);
                    _baseRepository.Update(Blog);
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
        public IActionResult DeleteBlog(int id)
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
