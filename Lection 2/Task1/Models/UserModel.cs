using System.ComponentModel.DataAnnotations;

namespace Task1.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

    }
}
