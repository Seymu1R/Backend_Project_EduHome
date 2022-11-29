using Microsoft.AspNetCore.Mvc;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
