using EduHomeProject.Areas.Admin.Models.Blog;
using EduHomeProject.Areas.Admin.Models.Event;
using EduHomeProject.Areas.Admin.Models.SpeakerEvent;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class SpeakerEventController : BaseController
    {
        private readonly AppDbContext _dbContext;
        public SpeakerEventController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<SpeakerEvent> list = await _dbContext.SpeakerEvents.Include(e=>e.Event).Include(s=>s.Speaker).ToListAsync();
            return View(list);
        }
        public async Task<IActionResult> Create() 
        {
            List<Speaker> speakers = await _dbContext.Speakers.ToListAsync();
            List<Event> events = await _dbContext.Events.ToListAsync();
            var categorySpeakersItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Speaker--", "0")
            };
            var categoryEventsItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Event--", "0")
            };
            speakers.ForEach(category => categorySpeakersItems.Add(new SelectListItem(category.FullName, category.Id.ToString())));
            events.ForEach(category => categoryEventsItems.Add(new SelectListItem(category.Title, category.Id.ToString())));

            SpeakerEventCreateModelView viewmodel = new SpeakerEventCreateModelView
            {
                SpeakerList = categorySpeakersItems,
                EventList=categoryEventsItems
            };
            return View(viewmodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SpeakerEventCreateModelView model)
        {
            SpeakerEvent speakerevent = new SpeakerEvent();
            Speaker speaker = await _dbContext.Speakers.Where(cate => cate.Id == model.SpeakerId).SingleOrDefaultAsync();
            Event eventitem = await _dbContext.Events.Where(cate => cate.Id == model.EventId).SingleOrDefaultAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            speakerevent.Speaker = speaker;
            speakerevent.Event = eventitem;

            _dbContext.SpeakerEvents.Add(speakerevent);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit()
        {
            List<Speaker> speakers = await _dbContext.Speakers.ToListAsync();
            List<Event> events = await _dbContext.Events.ToListAsync();
            var categorySpeakersItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Speaker--", "0")
            };
            var categoryEventsItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Event--", "0")
            };
            speakers.ForEach(category => categorySpeakersItems.Add(new SelectListItem(category.FullName, category.Id.ToString())));
            events.ForEach(category => categoryEventsItems.Add(new SelectListItem(category.Title, category.Id.ToString())));

            SpeakerEventCreateModelView viewmodel = new SpeakerEventCreateModelView
            {
                SpeakerList = categorySpeakersItems,
                EventList = categoryEventsItems
            };
            return View(viewmodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, SpeakerEventCreateModelView model)
        {
            if (id is null) return BadRequest();
            SpeakerEvent speakerevent = await _dbContext.SpeakerEvents.FindAsync(id);
            if(speakerevent is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            speakerevent.Speaker= await _dbContext.Speakers.FindAsync(model.SpeakerId);
            speakerevent.Event = await _dbContext.Events.FindAsync(model.EventId);

            await  _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
