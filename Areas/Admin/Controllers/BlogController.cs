using EduHomeProject.Areas.Admin.Data;
using EduHomeProject.Areas.Admin.Models.Blog;
using EduHomeProject.Areas.Admin.Models.Course;
using EduHomeProject.Areas.Admin.Models.Event;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class BlogController :BaseController
    {
        private readonly AppDbContext _dbContext; 

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _dbContext.Blogs.Include(C => C.Category).ToListAsync();
            return View(blogs);
        }
        public async Task<IActionResult> Create() 
        {
            List<Category> categories = await _dbContext.Categories.ToListAsync();

            var categoryListItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Category--", "0")
            };

            categories.ForEach(category => categoryListItems.Add(new SelectListItem(category.Name, category.Id.ToString())));
            CreateBlogViewModel viewmodel = new CreateBlogViewModel
            {
                CategoryList = categoryListItems
            };

            return View(viewmodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateBlogViewModel model)
        {
            Blog blog = new Blog();

            Category category = await _dbContext.Categories.Where(cate => cate.Id == model.CategoryId).SingleOrDefaultAsync();

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
            string unicalName = await model.Image.GenerateFile(Constants.BlogImagePath);

            blog.Title = model.Title;
            blog.Content = model.Content;
            blog.Category = category;
            blog.ImageUrl = unicalName;

            await _dbContext.Blogs.AddAsync(blog);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Blog blog = await _dbContext.Blogs.FindAsync(id);

            List<Category> categories = await _dbContext.Categories.ToListAsync();

            var categoryListItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Category--", "0")
            };

            categories.ForEach(category => categoryListItems.Add(new SelectListItem(category.Name, category.Id.ToString())));

            CreateBlogViewModel blogmodel = new CreateBlogViewModel
            {
                Title = blog.Title,
                Content = blog.Content,               
                CategoryList = categoryListItems,

            };

            return View(blogmodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, CreateBlogViewModel model)
        {
            if (id is null) return BadRequest();

            Blog blog = await _dbContext.Blogs.FindAsync(id);

            Category category = await _dbContext.Categories.FindAsync(model.CategoryId);


            List<Category> categories = await _dbContext.Categories.ToListAsync();

            var categoryListItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Category--", "0")
            };

            categories.ForEach(category => categoryListItems.Add(new SelectListItem(category.Name, category.Id.ToString())));

            CreateBlogViewModel eventemodel = new CreateBlogViewModel
            {
                CategoryList = categoryListItems
            };

            if (!ModelState.IsValid)
            {
                return View(eventemodel);
            }
            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Photo cannot be larger than 10 mb.");
                return View(eventemodel);
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "The photo is not suitable");
                return View(eventemodel);
            }
            string imagepath = Path.Combine(Constants.BlogImagePath, blog.ImageUrl);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            string unicalName = await model.Image.GenerateFile(Constants.BlogImagePath);

           
            blog.Category = category;
            blog.ImageUrl = unicalName;
            blog.Content = model.Content;
            blog.Title = model.Title;

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Blog blog = await _dbContext.Blogs.FindAsync(id);
            if (blog == null) return NotFound();

            string imagepath = Path.Combine(Constants.BlogImagePath, blog.ImageUrl);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            

            _dbContext.Blogs.Remove(blog);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
