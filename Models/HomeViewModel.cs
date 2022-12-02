using EduHomeProject.DAL.Entities;

namespace EduHomeProject.Models
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; } = new();
        public List<Course> Courses { get; set; } = new();
        public List<Event> Events { get; set; } = new();
        public List<Blog> Blogs { get; set; } = new();
    }
}
