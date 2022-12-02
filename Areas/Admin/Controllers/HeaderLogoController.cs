using EduHomeProject.Areas.Admin.Data;
using EduHomeProject.Areas.Admin.Models;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class HeaderLogoController : BaseController
    {
        private readonly AppDbContext _dbCobtext;
        public HeaderLogoController(AppDbContext dbContext)
        {
            _dbCobtext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<HeaderLogo> headerLogos = await _dbCobtext.HeaderLogos.ToListAsync();
            return View(headerLogos);
        }
        public  IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateHeaderLogoViewModel model) 
        {
            HeaderLogo header = new HeaderLogo();

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Şəklin Hecmi 10 mb- dan boyuk ola bilmez");
                return View();
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "Şəkil uyğun deyil");
                return View();
            }

            string unicalName = await model.Image.GenerateFile(Constants.HeaderLogoImagePath);

            header.ImageUrl = unicalName;          

            await _dbCobtext.HeaderLogos.AddAsync(header);
            await _dbCobtext.SaveChangesAsync();
            
           return  RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            HeaderLogo headerLogo = await _dbCobtext.HeaderLogos.FindAsync(id);

            if (headerLogo == null) return NotFound();

            string imagepath = Path.Combine(Constants.HeaderLogoImagePath, headerLogo.ImageUrl);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            _dbCobtext.HeaderLogos.Remove(headerLogo);

            await _dbCobtext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            HeaderLogo header = await _dbCobtext.HeaderLogos.FindAsync(id);

            if (header is null) return NotFound();         
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, CreateHeaderLogoViewModel model)
        {
            if (id == null) return NotFound();
            HeaderLogo headerLogo = await _dbCobtext.HeaderLogos.FindAsync(id);
            if (headerLogo is null) return NotFound();
            if (headerLogo.Id != id) return BadRequest();            

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Şəklin Hecmi 10 mb- dan boyuk ola bilmez");
                return View();
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "Şəkil uyğun deyil");
                return View();
            }


            string imagepath = Path.Combine(Constants.HeaderLogoImagePath, headerLogo.ImageUrl);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }

            string unicalName = await model.Image.GenerateFile(Constants.HeaderLogoImagePath);
           
            headerLogo.ImageUrl = unicalName;

            await _dbCobtext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
