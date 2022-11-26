namespace EduHomeProject.DAL.Entities
{
    public class TeacherSkill:BaseEntity
    {
        public byte Language { get; set; }
        public byte TeamLeader { get; set; }
        public byte Development { get; set; }
        public byte Design { get; set; }
        public byte Innovation { get; set; }
        public byte Communication { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }

    }
}
