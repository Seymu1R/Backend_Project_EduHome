using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.ViewComponents
{
    public class InformationViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public InformationViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Information> informations = await _dbContext.Informations.ToListAsync();
            return View(informations);
        }

    }
}
