using EduHomeProject.Areas.Admin.Data;
using EduHomeProject.Areas.Admin.Models.Course;
using EduHomeProject.Areas.Admin.Models.Event;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class EventController : BaseController
    {
        private readonly AppDbContext _dbcontext;

        public EventController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IActionResult> Index()
        {
            List<Event> eventlist = await _dbcontext.Events.Include(c=>c.Category).ToListAsync();
            return View(eventlist);
        }
        public async Task<IActionResult> Create()
        {
            List<Category> categories = await _dbcontext.Categories.ToListAsync();

            var categoryListItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Category--", "0")
            };

            categories.ForEach(category => categoryListItems.Add(new SelectListItem(category.Name , category.Id.ToString())));
           CreateEventModelView viewmodel = new CreateEventModelView
           {
                CategoryList = categoryListItems
            };

            return View(viewmodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateEventModelView model)
        {
            Event eventitem = new Event();

            Category category = await _dbcontext.Categories.Where(cate => cate.Id == model.CategoryId).SingleOrDefaultAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Şəklin Hecmi 10 mb- dan boyuk ola bilmez");
                return View();
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "Şəkil uyğun deyil");
                return View();
            }
            string unicalName = await model.Image.GenerateFile(Constants.EventImagePath);

            eventitem.Duration = model.Duration;
            eventitem.Location = model.Location;
            eventitem.Content = model.Content;
            eventitem.Title= model.Title;
            eventitem.StartTime= model.StartTime;
            eventitem.ImageUrl = unicalName;
            eventitem.Category = category; 

            await _dbcontext.Events.AddAsync(eventitem);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Event eventitem = await _dbcontext.Events.FindAsync(id);
            List<Category> categories = await _dbcontext.Categories.ToListAsync();

            var categoryListItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Category--", "0")
            };

            categories.ForEach(category => categoryListItems.Add(new SelectListItem(category.Name, category.Id.ToString())));

            CreateEventModelView coursemodel = new CreateEventModelView
            {
                Title = eventitem.Title,
                StartTime= eventitem.StartTime,
                Duration= eventitem.Duration,
                Location = eventitem.Location,
                Content = eventitem.Content,
                CategoryList = categoryListItems,

            };

            return View(coursemodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, CreateEventModelView model)
        {
            if (id is null) return BadRequest();

            Event eventitem = await _dbcontext.Events.FindAsync(id);

            Category category = await _dbcontext.Categories.FindAsync(model.CategoryId);


            List<Category> categories = await _dbcontext.Categories.ToListAsync();

            var categoryListItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Category--", "0")
            };

            categories.ForEach(category => categoryListItems.Add(new SelectListItem(category.Name, category.Id.ToString())));

            CreateEventModelView eventemodel = new CreateEventModelView
            {
                CategoryList = categoryListItems
            };

            if (!ModelState.IsValid)
            {
                return View(eventemodel);
            }
            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Şəklin Hecmi 10 mb- dan boyuk ola bilmez");
                return View(eventemodel);
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "Şəkil uyğun deyil");
                return View(eventemodel);
            }
            string imagepath = Path.Combine(Constants.CourseImagePath, eventitem.ImageUrl);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            string unicalName = await model.Image.GenerateFile(Constants.CourseImagePath);

            eventitem.Duration = model.Duration;
            eventitem.Location = model.Location;
            eventitem.Category = category;
            eventitem.ImageUrl = unicalName;
            eventitem.Content = model.Content;
            eventitem.StartTime = model.StartTime;
            eventitem.Title = model.Title;

            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Event eventitem = await _dbcontext.Events.FindAsync(id);
            if (eventitem == null) return NotFound();

            _dbcontext.Events.Remove(eventitem);

            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
