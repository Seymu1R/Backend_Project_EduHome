using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduHomeProject.DAL.Entities
{
    public class Teacher:BaseEntity
    {
        public string FullName { get; set; }

        public string Position { get; set; }

        public string AboutMe { get; set; }
        public string ImageUrl { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }

        public string Hobbies { get; set; }

        public string Faculty { get; set; }

        public string Mail { get; set; }

        public string Phone { get; set; }

        public string SkypeAdress { get; set; }
        public List<TeacherSkill> TeacherSkils { get; set; }
        public List<TeacherSosialMedia> TeacherSosialMedias { get; set; }

    }
}
