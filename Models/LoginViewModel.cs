using System.ComponentModel.DataAnnotations;

namespace EduHomeProject.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
        
    }
}
