namespace EduHomeProject.Areas.Admin.Models
{
    public class EditTeacherSosialViewModel
    {
        public string Name { get; set; }
        public string MediaLink { get; set; }
        public string ImageUrl { get; set; }=string.Empty;
        public IFormFile image { get; set; }
    }
}
