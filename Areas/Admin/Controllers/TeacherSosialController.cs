using EduHomeProject.Areas.Admin.Data;
using EduHomeProject.Areas.Admin.Models;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.DependencyResolver;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class TeacherSosialController : BaseController
    {
        private AppDbContext _dbcontext;
        public TeacherSosialController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IActionResult> Index()
        {
            List<TeacherSosialMedia> lists = await _dbcontext.TeacherSosialMedias.Include(t => t.Teacher).ToListAsync();
            return View(lists);
        }
        public async Task<IActionResult> Create()
        {
            List<Teacher> Teacherslist = await _dbcontext.Teachers.Where(x => !x.IsDeleted).ToListAsync();

            var teacherListItems = new List<SelectListItem>
            {
                new SelectListItem("--Select Teacher--", "0")
            };

            Teacherslist.ForEach(category => teacherListItems.Add(new SelectListItem(category.FullName, category.Id.ToString())));

            CreateTeacherSosialMediaViewModel model = new CreateTeacherSosialMediaViewModel { TeacherCategories = teacherListItems };

            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateTeacherSosialMediaViewModel model)
        {
            TeacherSosialMedia sosialmedia = new TeacherSosialMedia();

            Teacher teacher = _dbcontext.Teachers.Where(x => x.Id == model.TeacherId).SingleOrDefault();
            if (teacher == null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!model.MediaLogo.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Photo cannot be larger than 10 mb.");
                return View();
            }
            if (!model.MediaLogo.IsImage())
            {
                ModelState.AddModelError("", "The photo is not suitable");
                return View();
            }
            if (await _dbcontext.Teachers.AnyAsync(teacher => teacher.Id == model.TeacherId) &&
                await _dbcontext.TeacherSosialMedias.AnyAsync(ts => ts.TeacherId == model.TeacherId))
            {
                ModelState.AddModelError("", "One teacher cannot be selected twice");
                return View();
            }
            string unicalName = await model.MediaLogo.GenerateFile(Constants.TeacherSosialLogoPath);

            sosialmedia.MediaLink = model.MediaLink;
            sosialmedia.MediaLogo = unicalName;
            sosialmedia.Teacher = teacher;
            sosialmedia.Name=model.Name;

            await _dbcontext.TeacherSosialMedias.AddAsync(sosialmedia);
            _dbcontext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            TeacherSosialMedia SosialMedia = await _dbcontext.TeacherSosialMedias.FindAsync(id);

            if (SosialMedia is null) return NotFound();

            EditTeacherSosialViewModel model = new EditTeacherSosialViewModel
            {
                MediaLink = SosialMedia.MediaLink,
                ImageUrl = SosialMedia.MediaLogo,
                Name=SosialMedia.Name,
            };


            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, EditTeacherSosialViewModel model) 
        {
            if (id == null) return BadRequest();

            TeacherSosialMedia  editedsosial = await _dbcontext.TeacherSosialMedias.FindAsync(id);

            if (editedsosial is null) return NotFound();

            EditTeacherSosialViewModel modelsosial = new EditTeacherSosialViewModel
            {
                MediaLink = editedsosial.MediaLink,
                ImageUrl = editedsosial.MediaLogo,
                Name= editedsosial.Name

            };

            if (!ModelState.IsValid)
            {
                return View(modelsosial);
            }
            if (!model.image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Şəklin Hecmi 10 mb- dan boyuk ola bilmez");
                return View(modelsosial);
            }
            if (!model.image.IsImage())
            {
                ModelState.AddModelError("", "Şəkil uyğun deyil");
                return View(modelsosial);
            }

            string imagepath = Path.Combine(Constants.TeacherSosialLogoPath,editedsosial.MediaLogo);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            string unicalName = await model.image.GenerateFile(Constants.TeacherSosialLogoPath);

            editedsosial.MediaLink = model.MediaLink;
            editedsosial.MediaLogo = unicalName;
            editedsosial.Name= model.Name;

            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id) 
        {
            if (id is null) return BadRequest();

            TeacherSosialMedia sosial = await _dbcontext.TeacherSosialMedias.FindAsync(id);

            if(sosial == null) return NotFound();

            string imagepath = Path.Combine(Constants.TeacherSosialLogoPath , sosial.MediaLogo);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }

             _dbcontext.TeacherSosialMedias.Remove(sosial);

            await _dbcontext.SaveChangesAsync();

            return  RedirectToAction(nameof(Index));
        }

    }
}
