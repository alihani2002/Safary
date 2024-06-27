using Domain.Entities;
using Domain.Repositories;
using Persistence.Data;
using Persistence.Repositories;
using Service.Abstractions;

namespace Safary.Repository
{
    public class BlogService: BaseRepository<Blog>, IBlogService
    {
        public readonly IUnitOfWork _unitOfWork;

        public BlogService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Blog?> ToggleStatus(int id)
        {
            var blog = await _unitOfWork.Blogs.GetById(id);

            if (blog is null) return null;

            blog.IsDeleted = !blog.IsDeleted;

            _unitOfWork.Complete();

            return blog;
        }
    }
}
