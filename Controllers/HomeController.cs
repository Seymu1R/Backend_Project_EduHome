using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EduHomeProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _dbContext.Sliders.ToListAsync();
            List<Course> courses = await _dbContext.Courses.Take(3).ToListAsync();
            List<Event> events= await _dbContext.Events.OrderByDescending(e=>e.CreatedDate).ToListAsync();
            List<Blog> blogs = await _dbContext.Blogs.OrderByDescending(e => e.CreatedDate).Take(3).ToListAsync();
   
            HomeViewModel viewModel= new HomeViewModel 
            {
                Sliders= sliders,
                Courses= courses,
                Events= events,
                Blogs= blogs
            };
            return View(viewModel);
        }        
    }
}