using EduHomeProject.Areas.Admin.Data;
using EduHomeProject.Areas.Admin.Models;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class TeacherController : BaseController
    {
        private AppDbContext _dbContext;

        public TeacherController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _dbContext.Teachers.ToListAsync();
            return View(teachers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateTeacherViewModel model)
        {
            Teacher teacher = new Teacher();
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
            string unicalName = await model.Image.GenerateFile(Constants.TeacherImagePath);

            teacher.Position = model.Position;
            teacher.AboutMe = model.AboutMe;
            teacher.Degree = model.Degree;
            teacher.Faculty = model.Faculty;
            teacher.FullName = model.FullName;
            teacher.Hobbies = model.Hobbies;
            teacher.Experience = model.Experience;
            teacher.Mail = model.Mail;
            teacher.Phone = model.Phone;
            teacher.SkypeAdress = model.SkypeAdress;
            teacher.ImageUrl = unicalName;

            await _dbContext.Teachers.AddAsync(teacher);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Teacher teacher = await _dbContext.Teachers.FindAsync(id);
           
            if (teacher == null)return NotFound();            

            EditTeacherViewModel teachermodel = new EditTeacherViewModel
            {
                Position = teacher.Position,
                AboutMe = teacher.AboutMe,
                Degree = teacher.Degree,
                Faculty = teacher.Faculty,
                FullName = teacher.FullName,
                Hobbies = teacher.Hobbies,
                Experience = teacher.Experience,
                Mail = teacher.Mail,
                Phone = teacher.Phone,
                SkypeAdress = teacher.SkypeAdress,
                ImageUrl= teacher.ImageUrl,
                Id=teacher.Id


            };

            return View(teachermodel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit( int? id, EditTeacherViewModel model)
        {
            if (id is null) return BadRequest();

            Teacher editedteacher = await _dbContext.Teachers.FindAsync(id);

            if (editedteacher == null) return NotFound();

            EditTeacherViewModel teachermodel = new EditTeacherViewModel
            {
                Position = editedteacher.Position,
                AboutMe = editedteacher.AboutMe,
                Degree = editedteacher.Degree,
                Faculty = editedteacher.Faculty,
                FullName = editedteacher.FullName,
                Hobbies = editedteacher.Hobbies,
                Experience = editedteacher.Experience,
                Mail = editedteacher.Mail,
                Phone = editedteacher.Phone,
                SkypeAdress = editedteacher.SkypeAdress,
                ImageUrl= editedteacher.ImageUrl

            };

            if (!ModelState.IsValid)
            {
                return View(teachermodel);
            }
            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Şəklin Hecmi 10 mb- dan boyuk ola bilmez");
                return View(teachermodel);
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "Şəkil uyğun deyil");
                return View(teachermodel);
            }
            
            string imagepath = Path.Combine(Constants.TeacherImagePath, editedteacher.ImageUrl);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            string unicalName = await model.Image.GenerateFile(Constants.TeacherImagePath);
            editedteacher.Position = model.Position;
            editedteacher.AboutMe = model.AboutMe;
            editedteacher.Degree = model.Degree;
            editedteacher.Faculty = model.Faculty;
            editedteacher.FullName = model.FullName;
            editedteacher.Hobbies = model.Hobbies;
            editedteacher.Experience = model.Experience;
            editedteacher.Mail = model.Mail;
            editedteacher.Phone = model.Phone;
            editedteacher.SkypeAdress = model.SkypeAdress;
            editedteacher.ImageUrl = unicalName;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();

            Teacher teacher = await _dbContext.Teachers.FindAsync(id);

            if (teacher == null) return NotFound();

            string imagepath = Path.Combine(Constants.TeacherImagePath, teacher.ImageUrl);

            if (System.IO.File.Exists(imagepath))
            {
                System.IO.File.Delete(imagepath);
            }
            _dbContext.Teachers.Remove(teacher);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
