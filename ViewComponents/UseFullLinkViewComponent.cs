using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EduHomeProject.ViewComponents
{
    public class UseFullLinkViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbcontext;
        public UseFullLinkViewComponent(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<UseFullLink> useFullLinks = await _dbcontext.UseFullLinks.ToListAsync();
            return View(useFullLinks);
        }

    }
}
