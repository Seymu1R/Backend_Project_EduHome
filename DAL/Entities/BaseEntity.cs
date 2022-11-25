using System.Data;

namespace EduHomeProject.DAL.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; } 
        public string? CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
