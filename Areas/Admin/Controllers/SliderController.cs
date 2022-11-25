using Microsoft.AspNetCore.Mvc;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create() 
        {
            return View();
        }
    }
}
