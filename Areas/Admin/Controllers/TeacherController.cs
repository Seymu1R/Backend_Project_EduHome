using EduHomeProject.Areas.Admin.Data;
using EduHomeProject.Areas.Admin.Models;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                ModelState.AddModelError("", "Şəklin Hecmi 10 mb- dan boyuk ola bilmez");
                return View();
            }
            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "Şəkil uyğun deyil");
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
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return BadRequest();

            Teacher teacher = await _dbContext.Teachers.FindAsync(id);

            string imagepath = Path.Combine(Constants.TeacherImagePath, teacher.ImageUrl);

            CreateTeacherViewModel teachermodel = new CreateTeacherViewModel
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


            };

            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit( int id, CreateTeacherViewModel model)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
