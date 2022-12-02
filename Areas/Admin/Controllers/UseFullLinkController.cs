using EduHomeProject.Areas.Admin.Models.information;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class UseFullLinkController : BaseController
    {
        private readonly AppDbContext _dbcontext;
        public UseFullLinkController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IActionResult> Index()
        {
            List<UseFullLink> useFullLinks = await _dbcontext.UseFullLinks.ToListAsync();
            return View(useFullLinks);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateInformationViewModel information)
        {
            UseFullLink useFull = new UseFullLink();
            if (!ModelState.IsValid)
            {
                return View();
            }
            useFull.Name = information.Name;
            useFull.Link = information.Link;
            await _dbcontext.UseFullLinks.AddAsync(useFull);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            UseFullLink useFull= await _dbcontext.UseFullLinks.FindAsync(id);
            if (useFull == null) return NotFound();
            CreateInformationViewModel viewModel = new CreateInformationViewModel
            {
                Name = useFull.Name,
                Link = useFull.Link,
            };
            return View(viewModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, CreateInformationViewModel model)
        {
            if (id is null) return BadRequest();
            UseFullLink useFull = await _dbcontext.UseFullLinks.FindAsync(id);
            if (useFull == null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View();
            }
            useFull.Name = model.Name;
            useFull.Link = model.Link;
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            UseFullLink useFull = await _dbcontext.UseFullLinks.FindAsync(id);
            if (useFull == null) return NotFound();
            _dbcontext.UseFullLinks.Remove(useFull);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

