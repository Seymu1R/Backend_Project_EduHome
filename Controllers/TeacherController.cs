using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using EduHomeProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _dbContext;
        public TeacherController(AppDbContext dbcontext)
        {
            _dbContext=dbcontext;
        }
        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _dbContext.Teachers.Include(tso=>tso.TeacherSosialMedias).ToListAsync();
            return View(teachers);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            Teacher teacher = await _dbContext.Teachers.Include(tsi => tsi.TeacherSkils)
                .Include(tso => tso.TeacherSosialMedias)
                .Where(i => i.Id == id).SingleOrDefaultAsync();
            TeacherSkill teacherskill = await _dbContext.TeacherSkills.Where(ts=>ts.Teacher==teacher).SingleOrDefaultAsync();

            TeacherViewModel viewmodel = new TeacherViewModel
            {
                Teacher = teacher,
                teacherSkill = teacherskill
            };

            return View(viewmodel);
        }
    }
}
