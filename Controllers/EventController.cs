using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Controllers
{
    public class EventController:Controller
    {
        private readonly AppDbContext dbcontext;

        public EventController(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<IActionResult> Index() 
        {
            List<Event> events = await dbcontext.Events.ToListAsync();
            return View(events);
        }
        public async Task<IActionResult> Detail(int? id) 
        {
            if (id is null) return BadRequest();
            Event eventitem=await dbcontext.Events.Include(sp=>sp.SpeakerEvents)
                .ThenInclude(sp=>sp.Speaker)
                .Where(i=>i.Id==id).SingleOrDefaultAsync();
            if (eventitem is null) return NotFound();
            List<Category> categories= await dbcontext.Categories.ToListAsync();
            EventViewModel viewmodel = new EventViewModel 
            {
                Event= eventitem,
                Categories= categories                
            };
            return View(viewmodel);
        }
        public async Task<IActionResult> CategoryDetail(int? id)
        {
            if (id is null) return BadRequest();
            Category category = await dbcontext.Categories.FindAsync(id);
            if (category == null) return NotFound();
            List<Event> events = await dbcontext.Events.Where(c => c.Category.Name == category.Name).ToListAsync();
            return View(events);
        }
    }
}
