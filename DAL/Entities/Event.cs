namespace EduHomeProject.DAL.Entities
{
    public class Event:BaseEntity
    {
        public string Title { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }
        public string Content { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<SpeakerEvent> SpeakerEvents { get; set; }
    }
}
