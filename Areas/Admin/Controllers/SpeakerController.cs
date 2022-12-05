using EduHomeProject.Areas.Admin.Data;
using EduHomeProject.Areas.Admin.Models;
using EduHomeProject.Areas.Admin.Models.Speaker;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class SpeakerController : BaseController
    {
        private readonly AppDbContext _dbContext;
        public SpeakerController(AppDbContext context )
        {
            _dbContext=context;
        }
        public async Task<IActionResult> Index()
        {
            List<Speaker> speakerList = await _dbContext.Speakers.ToListAsync();
            return View(speakerList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateSpeakerViewModel model)
        {
            Speaker speaker = new Speaker();

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

            string unicalName = await model.Image.GenerateFile(Constants.SpeakerImagePath);

            speaker.FullName = model.FullName;
            speaker.Position = model.Position;
            speaker.ImageUrl = unicalName;

            await _dbContext.Speakers.AddAsync(speaker);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();

            Speaker speaker = await _dbContext.Speakers.FindAsync(id);

            if (speaker == null) return NotFound();

            string imagepath = Path.Combine(Constants.SpeakerImagePath, speaker.ImageUrl);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            _dbContext.Speakers.Remove(speaker);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Speaker speaker = await _dbContext.Speakers.FindAsync(id);

            if (speaker is null) return NotFound();

            CreateSpeakerViewModel slidermodel = new CreateSpeakerViewModel
            {
                FullName= speaker.FullName,
                Position=speaker.Position,

            };
            return View(slidermodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, CreateSpeakerViewModel model)
        {
            if (id == null) return NotFound();

            Speaker speaker = await _dbContext.Speakers.FindAsync(id);

            if (speaker is null) return NotFound();

            if (speaker.Id != id) return BadRequest();

            CreateSpeakerViewModel speakermodel = new CreateSpeakerViewModel
            {
                FullName = speaker.FullName,
                Position = speaker.Position                
            };

            if (!ModelState.IsValid)
            {
                return View(speakermodel);
            }
            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Photo cannot be larger than 10 mb.");
                return View(speakermodel);
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "The photo is not suitable");
                return View(speakermodel);
            }


            string imagepath = Path.Combine(Constants.SpeakerImagePath, speaker.ImageUrl);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }

            string unicalName = await model.Image.GenerateFile(Constants.SpeakerImagePath);
            speaker.FullName = model.FullName;
            speaker.Position=model.Position;
            speaker.ImageUrl = unicalName;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
