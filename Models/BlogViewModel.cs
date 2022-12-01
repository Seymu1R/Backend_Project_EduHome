using EduHomeProject.DAL.Entities;

namespace EduHomeProject.Models
{
    public class BlogViewModel
    {
        public List<Category> categories { get; set; }
        public Blog Blog { get; set; }
    }
}
