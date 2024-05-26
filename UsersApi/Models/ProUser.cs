using System.ComponentModel.DataAnnotations;

namespace UsersApi.Models
{
    public class ProUser : User
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public required string Password { get; set; }
    }
}
