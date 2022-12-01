using EduHomeProject.DAL.Entities;

namespace EduHomeProject.Models
{
    public class EventViewModel
    {
        public List<Category> Categories { get; set; }
        public Event Event { get; set; }
    }
}
