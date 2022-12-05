using Microsoft.AspNetCore.Mvc.Rendering;

namespace EduHomeProject.Areas.Admin.Models
{
    public class CreateTeacherSosialMediaViewModel
    {
        public string Name { get; set; }
        public string MediaLink { get; set; }
        public IFormFile MediaLogo { get; set; }        
        public int TeacherId { get; set; }
        public List<SelectListItem>? TeacherCategories { get; set; } = new();
    }
}
