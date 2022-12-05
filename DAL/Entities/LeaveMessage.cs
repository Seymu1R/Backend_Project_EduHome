using System.ComponentModel.DataAnnotations;

namespace EduHomeProject.DAL.Entities
{
    public class LeaveMessage:BaseEntity
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
