using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduHomeProject.Controllers
{
    public class HomeController : Controller
    {     
        public IActionResult Index()
        {
            return View();
        }        
    }
}