using Microsoft.AspNetCore.Mvc;

namespace EduHomeProject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
