namespace EduHomeProject.DAL.Entities
{
    public class Blog:BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
