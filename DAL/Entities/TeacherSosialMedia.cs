namespace EduHomeProject.DAL.Entities
{
    public class TeacherSosialMedia:BaseEntity
    {
        public string MediaLink { get; set; }
        public string MediaLogo { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
    }
}
