using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbcontext;
        public BlogController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
    
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _dbcontext.Blogs.ToListAsync();
            return View(blogs);
        }
        public async Task<IActionResult> Detail(int? id) 
        {
            if (id is null) return BadRequest();
            Blog blog = await _dbcontext.Blogs.FindAsync(id);
            if(blog == null) return NotFound();
            List<Category> categories = await _dbcontext.Categories.ToListAsync();
            BlogViewModel viewmodel = new BlogViewModel
            {
                categories= categories,
                Blog=blog
            };
            
            return View(viewmodel);            
        }
        public async Task<IActionResult> DetailCategory(int? id) 
        {
            if(id is null) return BadRequest();
            Category category = await _dbcontext.Categories.FindAsync(id);
            if(category == null) return NotFound();
            List<Blog> blogs = await _dbcontext.Blogs.Where(c => c.Category == category).ToListAsync();
            return View(blogs);
            
        }
    }
}
