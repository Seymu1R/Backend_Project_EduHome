using EduHomeProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHomeProject.ViewComponents
{
    public class SlideStudentViewComponent:ViewComponent
    {
        public async Task< IViewComponentResult> InvokeAsync()
        {            
            return  View();
        }
    }
}
