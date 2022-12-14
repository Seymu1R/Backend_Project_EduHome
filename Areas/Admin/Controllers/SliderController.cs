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
                ModelState.AddModelError("", "Photo cannot be larger than 10 mb.");
                return View();
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "The photo is not suitable");
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

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id) 
        {
            if (id is null) return BadRequest();          

            Slider slider = await _dbContext.Sliders.FindAsync(id);  
            
            if(slider is null) return NotFound();   

            EditSliderViewModel slidermodel = new EditSliderViewModel
            {
                Subtitle = slider.SubTitle,
                Title = slider.Title,
                Description=slider.Description,
                Id = slider.Id,
                ImageUrl= slider.ImageUrl

            };
            return View(slidermodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id,EditSliderViewModel model)
        {
            if (id == null) return NotFound();           

            Slider editedslider = await _dbContext.Sliders.FindAsync(id);

            if (editedslider is null) return NotFound();            

            if (editedslider.Id != id) return BadRequest();   
            
            EditSliderViewModel slidermodel = new EditSliderViewModel {
                Subtitle = editedslider.SubTitle,
                Title = editedslider.Title,
                Description = editedslider.Description,
                ImageUrl = editedslider.ImageUrl
               
            };
            
            if (!ModelState.IsValid)
            {
                return View(slidermodel);
            }
            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Photo cannot be larger than 10 mb.");
                return View(slidermodel);
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "The photo is not suitable");
                return View(slidermodel);
            }
           

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
