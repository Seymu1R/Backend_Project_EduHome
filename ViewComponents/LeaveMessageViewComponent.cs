using Microsoft.AspNetCore.Mvc;

namespace EduHomeProject.ViewComponents
{
    public class LeaveMessageViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
