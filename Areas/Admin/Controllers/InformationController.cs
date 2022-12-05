using EduHomeProject.Areas.Admin.Models.Blog;
using EduHomeProject.Areas.Admin.Models.information;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class InformationController : BaseController
    {
        private readonly AppDbContext _dbContext;
        public InformationController(AppDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Information> informations = await _dbContext.Informations.ToListAsync();

            return View(informations);
        }
        public  IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateInformationViewModel information)
        {
            Information info = new Information();

            if (!ModelState.IsValid) 
            {
                return View();
            }

            info.Name = information.Name;
            info.Link = information.Link;

            await _dbContext.Informations.AddAsync(info);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id) 
        {
            if (id is null) return BadRequest();

            Information info = await _dbContext.Informations.FindAsync(id);

            if (info == null) return NotFound();

            CreateInformationViewModel viewModel = new CreateInformationViewModel 
            {
                Name = info.Name,
                Link = info.Link,
            };

            return View(viewModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, CreateInformationViewModel model) 
        {
            if (id is null) return BadRequest();

            Information info = await _dbContext.Informations.FindAsync(id);

            if (info == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            info.Name = model.Name;
            info.Link = model.Link;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id) 
        { 
            if (id is null) return BadRequest();

            Information infod = await _dbContext.Informations.FindAsync(id);

            if (infod == null) return NotFound();

             _dbContext.Informations.Remove(infod);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
