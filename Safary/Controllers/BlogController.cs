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
            

            var blog = await _unitOfWork.Blogs.GetById(id);

            if(blog is null)
                return NotFound();

            return Ok(blog);
        }
        [HttpPost]
        public async Task<IActionResult> AddBlog(BlogPostDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var blog = _mapper.Map<Blog>(model);

            await _unitOfWork.Blogs.Add(blog);

            _unitOfWork.Complete();
            return Ok(blog);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog( int id,[FromForm] BlogPostDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBlog = await _unitOfWork.Blogs.GetById(id);

            if (existingBlog == null)
                return NotFound();

            _mapper.Map(dto, existingBlog);

            _unitOfWork.Blogs.Update(existingBlog);
            _unitOfWork.Complete();

            return Ok(existingBlog); 
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var existingBlog = await _unitOfWork.Blogs.GetById(id);

            if (existingBlog == null)
                return NotFound();

            _unitOfWork.Blogs.Remove(existingBlog);
            _unitOfWork.Complete();

            return Ok(existingBlog);
        }


    }
}
