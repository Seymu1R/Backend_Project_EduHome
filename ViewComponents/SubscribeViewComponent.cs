using EduHomeProject.DAL.Entities;
using EduHomeProject.DAL;
using Microsoft.AspNetCore.Mvc;
using EduHomeProject.Areas.Admin.Models;
using EduHomeProject.Models;

namespace EduHomeProject.ViewComponents
{
    public class SubscribeViewComponent:ViewComponent
    {        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            SubscribeViewModel model = new SubscribeViewModel();
            return View(model);
        }
    }
}
