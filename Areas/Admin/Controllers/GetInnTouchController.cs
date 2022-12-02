using EduHomeProject.Areas.Admin.Models.GetInTouch;
using EduHomeProject.Areas.Admin.Models.information;
using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.Areas.Admin.Controllers
{
    public class GetInnTouchController : BaseController
    {
        private readonly AppDbContext _dbcontext;
        public GetInnTouchController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IActionResult> Index()
        {
            List<GetInTouch> getInTouches =  await _dbcontext.GetInTouches.ToListAsync();
            return View(getInTouches);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateGetInTouchViewModel model)
        {
            GetInTouch getIn = new GetInTouch();
            if (!ModelState.IsValid)
            {
                return View();
            }
            getIn.PhoneContact = model.PhoneContact;
            getIn.WebAdress = model.WebAdress;
            getIn.Location = model.Location;
            await _dbcontext.GetInTouches.AddAsync(getIn);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            GetInTouch getin = await _dbcontext.GetInTouches.FindAsync(id);
            if (getin == null) return NotFound();
            CreateGetInTouchViewModel viewModel = new CreateGetInTouchViewModel
            {
                Location = getin.Location,
                WebAdress = getin.WebAdress,
                PhoneContact=getin.PhoneContact

            };
            return View(viewModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, CreateGetInTouchViewModel model)
        {
            if (id is null) return BadRequest();
            GetInTouch getIn = await _dbcontext.GetInTouches.FindAsync(id);
            if (getIn == null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View();
            }
            getIn.Location = model.Location;
            getIn.PhoneContact = model.PhoneContact;
            getIn.WebAdress = model.WebAdress;
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            GetInTouch getin = await _dbcontext.GetInTouches.FindAsync(id);
            if (getin == null) return NotFound();
            _dbcontext.GetInTouches.Remove(getin);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
