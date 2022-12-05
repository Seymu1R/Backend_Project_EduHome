using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class ContactWithMessageController : BaseController
    {
        private readonly AppDbContext _dbcontext;
        public ContactWithMessageController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IActionResult> Index()
        {
            List<LeaveMessage> leaveMessages = await _dbcontext.LeaveMessages.ToListAsync();

            return View(leaveMessages);
        }
    }
}
