using EduHomeProject.Areas.Admin.Data;
using EduHomeProject.Areas.Admin.Models;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        private readonly  AppDbContext _dbContext;

        public SliderController(AppDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        public async  Task<IActionResult> Index()
        {
            List<Slider> sliders = await _dbContext.Sliders.ToListAsync();
            return View(sliders);
        }
        public  IActionResult  Create() 
        {            
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public  async Task<IActionResult> Create(CreateSliderViewModel model)
        {
            Slider slider = new Slider();

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

            string unicalName = await model.Image.GenerateFile(Constants.SliderImagePath);

            slider.Title = model.Title;
            slider.SubTitle = model.Subtitle;
            slider.Description= model.Description;
            slider.ImageUrl = unicalName;

            await _dbContext.Sliders.AddAsync(slider);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Delete() 
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();

            Slider slider =await _dbContext.Sliders.FindAsync(id);

            if (slider == null) return NotFound();

            string imagepath = Path.Combine(Constants.SliderImagePath, slider.ImageUrl);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            _dbContext.Sliders.Remove(slider);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Delete));
        }

        public async Task<IActionResult> Edit(int id) 
        {
            if (id == null) return BadRequest();

            Slider slider = await _dbContext.Sliders.FindAsync(id);           

            EditSliderViewModel slidermodel = new EditSliderViewModel
            {
                Subtitle = slider.SubTitle,
                Title = slider.Title,
                Description=slider.Description,
                Id = slider.Id

            };
            return View(slidermodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id,EditSliderViewModel model)
        {
            Slider editedslider = await _dbContext.Sliders.FindAsync(id);

            string imagepath = Path.Combine(Constants.SliderImagePath, editedslider.ImageUrl);
            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }

            string unicalName = await model.Image.GenerateFile(Constants.SliderImagePath);
            editedslider.SubTitle = model.Subtitle;
            editedslider.Title = model.Subtitle;
            editedslider.Description= model.Description;
            editedslider.ImageUrl = unicalName;           

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
