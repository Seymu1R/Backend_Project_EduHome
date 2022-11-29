using Microsoft.EntityFrameworkCore;

namespace EduHomeProject.DAL.Entities
{
    
    public class Speaker:BaseEntity
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<SpeakerEvent> SpeakerEvents { get; set; }
    }
}
