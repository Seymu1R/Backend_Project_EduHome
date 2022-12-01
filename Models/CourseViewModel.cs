using EduHomeProject.DAL.Entities;

namespace EduHomeProject.Models
{
    public class CourseViewModel
    {
        public List<Category> Categories { get; set; }=new List<Category>();
        public Course Course { get; set; } = new();
    }
}
