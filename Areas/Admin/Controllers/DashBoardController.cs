using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class DashBoardController : BaseController
    {        
        public async Task<IActionResult> Index()
        {            
            return View();
        }
    }
}
