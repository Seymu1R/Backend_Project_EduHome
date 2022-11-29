namespace EduHomeProject.DAL.Entities
{
    public class SpeakerEvent:BaseEntity
    {
        public Speaker Speaker { get; set; }
        public int SpeakerId { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}
