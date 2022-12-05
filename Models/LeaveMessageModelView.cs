using System.ComponentModel.DataAnnotations;

namespace EduHomeProject.Models
{
    public class LeaveMessageModelView
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
