namespace EduHomeProject.Areas.Admin.Models.Speaker
{
    public class CreateSpeakerViewModel
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public IFormFile Image { get; set; }
    }
}
