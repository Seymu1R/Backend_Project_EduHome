using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EduHomeProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _dbContext.Sliders.ToListAsync();

            HomeViewModel viewModel= new HomeViewModel 
            {
                Sliders= sliders
            };
            return View(viewModel);
        }        
    }
}