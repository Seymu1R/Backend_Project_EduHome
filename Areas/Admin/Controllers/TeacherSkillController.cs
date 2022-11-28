using EduHomeProject.Areas.Admin.Models;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class TeacherSkillController : BaseController
    {
        private AppDbContext _dbcontext;

        public TeacherSkillController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IActionResult> Index()
        {
            List<TeacherSkill> lists = await _dbcontext.TeacherSkills.Include(teacher => teacher.Teacher).ToListAsync();
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

            CreateTeacherSkillViewModel model = new CreateTeacherSkillViewModel { TeacherCategories = teacherListItems };

            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateTeacherSkillViewModel model)
        {
            TeacherSkill teacherskil = new TeacherSkill();
            Teacher teacher = _dbcontext.Teachers.Where(x => x.Id == model.TeacherId).SingleOrDefault();

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _dbcontext.Teachers.AnyAsync(teacher => teacher.Id == model.TeacherId) &&
                await _dbcontext.TeacherSkills.AnyAsync(ts => ts.TeacherId == model.TeacherId))
            {
                ModelState.AddModelError("", "Bir muellim iki defe secile bilmez");
                return View();
            }

            teacherskil.Language = model.Language;
            teacherskil.Innovation = model.Innovation;
            teacherskil.Communication = model.Communication;
            teacherskil.TeamLeader = model.TeamLeader;
            teacherskil.Development = model.Development;
            teacherskil.Design = model.Design;
            teacherskil.Teacher = teacher;

            await _dbcontext.TeacherSkills.AddAsync(teacherskil);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            TeacherSkill teacherskil = await _dbcontext.TeacherSkills.FindAsync(id);

            if (teacherskil == null) return NotFound();

            _dbcontext.TeacherSkills.Remove(teacherskil);

            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            TeacherSkill teacherskill = await _dbcontext.TeacherSkills.FindAsync(id);

            if (teacherskill is null) return NotFound();

            EditTeacherSkillViewModel model = new EditTeacherSkillViewModel
            {
                Language = teacherskill.Language,
                Innovation = teacherskill.Innovation,
                Communication = teacherskill.Communication,
                TeamLeader = teacherskill.TeamLeader,
                Development = teacherskill.Development,
                Design = teacherskill.Design

            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, EditTeacherSkillViewModel model)
        {
            if (id is null) return BadRequest();

            TeacherSkill teacherskill = await _dbcontext.TeacherSkills.FindAsync(id);
          
            if (teacherskill is null) return NotFound();        

            if (!ModelState.IsValid) 
            {
                return View();
            }

            teacherskill.Language = model.Language;
            teacherskill.Innovation = model.Innovation;
            teacherskill.Communication = model.Communication;
            teacherskill.TeamLeader = model.TeamLeader;
            teacherskill.Development = model.Development;
            teacherskill.Design = model.Design;        
            
        
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
