using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.ViewComponents
{
    public class LastPostSideViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public LastPostSideViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Blog> blogs =  await _dbContext.Blogs
                .Where(blog => !blog.IsDeleted)
                .OrderByDescending(blog => blog.CreatedDate)
                .Take(3)
                .ToListAsync();
            return View(blogs);
        }
    }
}
