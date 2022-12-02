using EduHomeProject.DAL;
using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.ViewComponents
{
    public class GetInnTouchViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public GetInnTouchViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            GetInTouch getInTouch = await _dbContext.GetInTouches.FirstOrDefaultAsync();
            return View(getInTouch);
        }
    }
}
