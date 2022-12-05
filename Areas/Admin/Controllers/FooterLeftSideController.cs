using EduHomeProject.Areas.Admin.Data;
using EduHomeProject.Areas.Admin.Models;
using EduHomeProject.Areas.Admin.Models.FooterLeftSide;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class FooterLeftSideController : BaseController
    {
        private readonly AppDbContext _dbContext;
        public FooterLeftSideController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        public async Task<IActionResult> Index()
        {
            List<FooterLeftSide> footerLeftSides = await _dbContext.FooterLeftSides.ToListAsync();
            return View(footerLeftSides);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(EditFooterInformationViewModel model)
        {
            FooterLeftSide footerLeftSide = new FooterLeftSide();

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
            string unicalName = await model.Image.GenerateFile(Constants.FooterLeftSideImagePath);

            footerLeftSide.Logo = unicalName;
            footerLeftSide.Content = model.Content;

            await _dbContext.FooterLeftSides.AddAsync(footerLeftSide);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            FooterLeftSide footerLeftSide = await _dbContext.FooterLeftSides.FindAsync(id);

            if (footerLeftSide == null) return NotFound();

            string imagepath = Path.Combine(Constants.FooterLeftSideImagePath, footerLeftSide.Logo);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            _dbContext.FooterLeftSides.Remove(footerLeftSide);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            FooterLeftSide footerLeftSide = await _dbContext.FooterLeftSides.FindAsync(id);

            if (footerLeftSide is null) return NotFound();

            EditFooterInformationViewModel footerInformationViewModel = new EditFooterInformationViewModel
            {
                Content = footerLeftSide.Content               

            };
            return View(footerInformationViewModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, EditFooterInformationViewModel model)
        {
            if (id == null) return NotFound();

            FooterLeftSide footerLeftSide = await _dbContext.FooterLeftSides.FindAsync(id);

            if (footerLeftSide is null) return NotFound();

            EditFooterInformationViewModel footerInformationViewModel = new EditFooterInformationViewModel
            {
               Content= footerLeftSide.Content
            };

            if (!ModelState.IsValid)
            {
                return View(footerInformationViewModel);
            }
            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Şəklin Hecmi 10 mb- dan boyuk ola bilmez");
                return View(footerInformationViewModel);
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "Şəkil uyğun deyil");
                return View(footerInformationViewModel);
            }


            string imagepath = Path.Combine(Constants.FooterLeftSideImagePath, footerLeftSide.Logo);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }

            string unicalName = await model.Image.GenerateFile(Constants.FooterLeftSideImagePath);
            footerLeftSide.Content = model.Content;
            footerLeftSide.Logo= unicalName;           

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
