using Microsoft.AspNetCore.Identity;

namespace EduHomeProject.DAL.Entities
{
    public class User:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
