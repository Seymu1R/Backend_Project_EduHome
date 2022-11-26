namespace EduHomeProject.Areas.Admin.Models
{
    public class CreateSliderViewModel
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }
    }
}
