using System.ComponentModel.DataAnnotations;

namespace WebAPITesting.Dtos.User
{
    public class UserAccountDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20,ErrorMessage = "La contrasena debe contener {1} caracteres como maximo y {2} como minimo", MinimumLength = 4)]
        public string Password { get; set; }
    }
}
