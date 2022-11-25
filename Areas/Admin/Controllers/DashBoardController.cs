using Microsoft.AspNetCore.Mvc;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class DashBoardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
