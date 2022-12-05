using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHomeProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _dbContext;
        public ContactController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SendMessage(LeaveMessageModelView model)
        {
            LeaveMessage message= new LeaveMessage();
            if (!ModelState.IsValid) 
            {
                return View(model);
            }
            message.Subject=model.Subject;
            message.Email = model.Email;
            message.Message=model.Message;
            message.Name=model.Name;
            await _dbContext.LeaveMessages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
