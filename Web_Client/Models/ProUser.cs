using System.ComponentModel.DataAnnotations;

namespace Web_Client.Models
{
    public class ProUser : Users
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public required string Password { get; set; }
    }
}
