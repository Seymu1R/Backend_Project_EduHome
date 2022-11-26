namespace EduHomeProject.Areas.Admin.Models
{
    public class EditSliderViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
