using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Controllers
{
    public class CourseController : Controller
    {
        private AppDbContext _dbContext;

        public CourseController(AppDbContext dbContext)
        {
            _dbContext=dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<Course> courses = await _dbContext.Courses.ToListAsync();            
            
            return View(courses);
        }

        public async Task<IActionResult> Detail(int? id) 
        {
            if (id is null) return BadRequest();

            Course course = await _dbContext.Courses.FindAsync(id);

            if (course == null) return NotFound();

            List<Category> categories = await _dbContext.Categories.ToListAsync();

            CourseViewModel viewmodel = new CourseViewModel 
            {
                Course = course,
                Categories = categories
            };

            return View(viewmodel);
        }
        public async Task<IActionResult> CategoryDetail(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _dbContext.Categories.FindAsync(id);

            if (category == null) return NotFound();

            List<Course> courses = await _dbContext.Courses.Where(c => c.Category.Name == category.Name).ToListAsync();

            return View(courses);
        }
        public async Task<IActionResult> Search(string searchText) 
        {
            List<Course> courses = await _dbContext.Courses
                .Where(x => x.Title.ToLower()
                .Contains(searchText
                .ToLower())).ToListAsync();

            return PartialView("_SearchedCoursePartial", courses);
        }
    }
}
