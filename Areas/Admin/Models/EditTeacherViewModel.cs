namespace EduHomeProject.Areas.Admin.Models
{
    public class EditTeacherViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Position { get; set; }

        public string AboutMe { get; set; }

        public string Degree { get; set; }
        public string Experience { get; set; }

        public string Hobbies { get; set; }

        public string Faculty { get; set; }

        public string Mail { get; set; }

        public string Phone { get; set; }

        public string SkypeAdress { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
    }
}
