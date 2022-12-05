using EduHomeProject.Areas.Admin.Data;
using EduHomeProject.Areas.Admin.Models;
using EduHomeProject.Areas.Admin.Models.Course;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Reflection.Metadata;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class CourseController : BaseController
    {
        private AppDbContext _dbcontext;

        public CourseController(AppDbContext dbContext)
        {
            _dbcontext=dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<Course> courses = await _dbcontext.Courses.Include(cate=>cate.Category).ToListAsync();
            return View(courses);
        }
        public async Task<IActionResult> Create() 
        {
            List<Category> categories = await _dbcontext.Categories.ToListAsync();

            var categoryListItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Category--", "0")
            };

            categories.ForEach(category => categoryListItems.Add(new SelectListItem(category.Name, category.Id.ToString())));
            CreateCourseViewModel viewmodel = new CreateCourseViewModel
            {
                CategoryList = categoryListItems
            };

            return View(viewmodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateCourseViewModel model)
        {
            Course course = new Course(); 
            
            Category category = await _dbcontext.Categories.Where(cate => cate.Id == model.CategoryId).SingleOrDefaultAsync();            

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Photo cannot be larger than 10 mb.");
                return View();
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "The photo is not suitable");
                return View();
            }
            string unicalName = await model.Image.GenerateFile(Constants.CourseImagePath);

            course.Assesment = model.Assesment;
            course.HowToApply = model.HowToApply;
            course.About = model.About;
            course.Duration = model.Duration;
            course.ClassDuration = model.ClassDuration;
            course.Certification = model.Certification;
            course.Description = model.Description;
            course.Language = model.Language;
            course.Price= model.Price;
            course.Title= model.Title;
            course.StartTime= model.StartTime;
            course.SkillLevel= model.SkillLevel;
            course.Students= model.Students;
            course.Category = category;
            course.ImageUrl = unicalName;

            await _dbcontext.Courses.AddAsync(course);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Course course = await _dbcontext.Courses.FindAsync(id);

            List<Category> categories = await _dbcontext.Categories.ToListAsync();

            var categoryListItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Category--", "0")
            };

            categories.ForEach(category => categoryListItems.Add(new SelectListItem(category.Name, category.Id.ToString())));          

            CreateCourseViewModel coursemodel = new CreateCourseViewModel
            {
                Title = course.Title,
                Assesment = course.Assesment,
                HowToApply = course.HowToApply,
                About = course.About,
                Duration = course.Duration,
                ClassDuration = course.ClassDuration,
                Certification = course.Certification,
                Description = course.Description,
                Language = course.Language,
                Price = course.Price,
                StartTime = course.StartTime,
                SkillLevel = course.SkillLevel,
                Students = course.Students,
                CategoryList = categoryListItems,

            };

            return View(coursemodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id,CreateCourseViewModel model)
        {
            if (id is null) return BadRequest();

            Course course = await _dbcontext.Courses.FindAsync(id);

            Category category = await _dbcontext.Categories.FindAsync(model.CategoryId);
            

            List<Category> categories = await _dbcontext.Categories.ToListAsync();

            var categoryListItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Category--", "0")
            };

            categories.ForEach(category => categoryListItems.Add(new SelectListItem(category.Name, category.Id.ToString())));

            CreateCourseViewModel coursemodel = new CreateCourseViewModel
            {                
                CategoryList = categoryListItems
            };

            if (!ModelState.IsValid)
            {
                return View(coursemodel);
            }
            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Photo cannot be larger than 10 mb.");
                return View(coursemodel);
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "The photo is not suitable");
                return View(coursemodel);
            }
            string imagepath = Path.Combine(Constants.CourseImagePath, course.ImageUrl);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            string unicalName = await model.Image.GenerateFile(Constants.CourseImagePath);

            course.Assesment = model.Assesment;
            course.HowToApply = model.HowToApply;
            course.About = model.About;
            course.Duration = model.Duration;
            course.ClassDuration = model.ClassDuration;
            course.Certification = model.Certification;
            course.Description = model.Description;
            course.Language = model.Language;
            course.Price = model.Price;
            course.Title = model.Title;
            course.StartTime = model.StartTime;
            course.SkillLevel = model.SkillLevel;
            course.Students = model.Students;
            course.Category = category;
            course.ImageUrl = unicalName;

            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id) 
        {
            Course course = await _dbcontext.Courses.FindAsync(id);
            if (course == null) return NotFound();

            string imagepath = Path.Combine(Constants.CourseImagePath, course.ImageUrl);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }

            _dbcontext.Courses.Remove(course);

            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
