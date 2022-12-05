using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.ViewComponents
{
    public class FooterLeftSideViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public FooterLeftSideViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           FooterLeftSide footerLeft = await _dbContext.FooterLeftSides.FirstOrDefaultAsync();            
           return View(footerLeft);
        }

    }
}
