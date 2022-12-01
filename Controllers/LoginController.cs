using Microsoft.AspNetCore.Mvc;

namespace EduHomeProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
