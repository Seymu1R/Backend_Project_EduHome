using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EduHomeProject.ViewComponents
{
    public class HeaderLogoViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbcontext;
        public HeaderLogoViewComponent(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderLogo logo = await _dbcontext.HeaderLogos.FirstOrDefaultAsync();
            return View(logo);
        }
    }
}
