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
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;   
        private readonly IMapper _mapper;


		public BlogController(IMapper mapper, IUnitOfWork unitOfWork)
		{
            _mapper = mapper;
			_unitOfWork = unitOfWork;
		}

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetBlogs()
        {
            var blogs = await _unitOfWork.Blogs.FindAll(s => s.Id > 0, include: s => s.Include(x => x.Tours));
            var dto = _mapper.Map<IEnumerable<BlogDTO>>(blogs);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBlogById(int id)
        {
            //try
            //{
            //    var Blog = _baseRepository.GetById(id);
            //    if (Blog == null)
            //    {
            //        return NotFound();
            //    }
            //    return Ok(Blog);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"Internal server error: {ex.Message}");
            //}

            var blog = await _unitOfWork.Blogs.GetById(id);

            if(blog is null)
                return NotFound();

            return Ok(blog);
        }
        [HttpPost]
        public async Task<IActionResult> AddBlog(BlogDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var blog = _mapper.Map<Blog>(model);

            await _unitOfWork.Blogs.Add(blog);

            _unitOfWork.Complete();
            return Ok(blog);
        }

        //[HttpPost]
        //public ActionResult<Blog> AddBlog(BlogPostDTO dto)
        //{
        //    //try
        //    //{
        //    //    if (dto != null)
        //    //    {
        //    //        var Blog = _mapper.Map<Blog>(dto);
        //    //        var createdBlog = _baseRepository.Add(Blog);
        //    //        return CreatedAtAction(nameof(GetBlog), new { id = createdBlog.Id }, createdBlog);
        //    //    }
        //    //    else { return BadRequest("Somethingwent wrong"); }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return StatusCode(500, $"Internal server error: {ex.Message}");
        //    //}
        //}

        //[HttpPut("{id}")]
        //public IActionResult PutBlog(int id, BlogDTO dto)
        //{
        //    try
        //    {
        //        if (id != dto.Id)
        //        {
        //            return BadRequest();
        //        }
        //        if (dto != null)
        //        {
        //            var Blog = _mapper.Map<Blog>(dto);
        //            _baseRepository.Update(Blog);
        //            return Ok();
        //        }
        //        else { return BadRequest("Somethingwent wrong"); }

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteBlog(int id)
        //{
        //    try
        //    {
        //        _baseRepository.Remove(_baseRepository.GetById(id));
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
    }
}
