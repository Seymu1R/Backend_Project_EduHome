using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EduHomeProject.Areas.Admin.Models.Event
{
    public class CreateEventModelView
    {
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Duration { get; set; }       
        public string Location { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem> CategoryList { get; set; } = new();

    }
}
