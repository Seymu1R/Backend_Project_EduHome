using Microsoft.CodeAnalysis.Options;

namespace EduHomeProject.DAL.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; } 
        public ICollection<Event> Events { get; set; } 
        public ICollection<Blog>
 Blogs { get; set; } 
    }
}
