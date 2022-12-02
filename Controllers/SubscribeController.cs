using EduHomeProject.Areas.Admin.Models;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHomeProject.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _dbcontext;
        public SubscribeController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }    
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SendEmail(SubscribeViewModel model)
        {
            Subscribe subscribe= new Subscribe();
            subscribe.EmailAdress = model.Email;
            await _dbcontext.Subscribes.AddAsync(subscribe);
            await _dbcontext.SaveChangesAsync();
            return Redirect("/Home/Index");
        }
    }
}
