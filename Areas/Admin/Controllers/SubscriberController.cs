using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class SubscriberController : BaseController
    {
        private readonly AppDbContext _dbcontext;
        public SubscriberController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IActionResult> Index()
        {
            List<Subscribe> subscribes = await _dbcontext.Subscribes.ToListAsync();
            return View(subscribes);
        }

        public async Task<IActionResult> Delete(int? id )
        {
            if (id is null) return BadRequest();
            Subscribe subscribe =  await _dbcontext.Subscribes.FindAsync(id);
            if (subscribe == null) return NotFound();
            _dbcontext.Subscribes.Remove(subscribe);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));                
           
        }
    }
}
