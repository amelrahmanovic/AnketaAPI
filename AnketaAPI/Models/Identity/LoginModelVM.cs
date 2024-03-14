using System.ComponentModel.DataAnnotations;

namespace AnketaAPI.Models.Identity
{
    public class LoginModelVM
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
