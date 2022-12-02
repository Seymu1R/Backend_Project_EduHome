using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _dbcontext;
        public static string Position="Professor";
        public AboutController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        
        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers=await _dbcontext.Teachers.Where(T => T.Position == Position).Take(4).ToListAsync();
            return View(teachers);
        }
    }
}
