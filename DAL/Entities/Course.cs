using Microsoft.CodeAnalysis.Options;

namespace EduHomeProject.DAL.Entities
{
    public class Course:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string About { get; set; }
        public string HowToApply { get; set; }
        public string Certification { get; set; }
        public string StartTime { get; set; }
        public int Duration { get; set; }
        public int ClassDuration { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int Students { get; set; }
        public string Assesment { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
