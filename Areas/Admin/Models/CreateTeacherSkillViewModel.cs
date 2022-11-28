using EduHomeProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EduHomeProject.Areas.Admin.Models
{
    public class CreateTeacherSkillViewModel
    {
        public byte Language { get; set; }
        public byte TeamLeader { get; set; }
        public byte Development { get; set; }
        public byte Design { get; set; }
        public byte Innovation { get; set; }
        public byte Communication { get; set; }
        public int TeacherId { get; set; }
        public List<SelectListItem>? TeacherCategories { get; set; } = new();
        
    }
}
