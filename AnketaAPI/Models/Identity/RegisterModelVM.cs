using System.ComponentModel.DataAnnotations;

namespace AnketaAPI.Models.Identity
{
    public class RegisterModelVM
    {
        public string? Username { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public List<string>? UserRoles { get; set; }
    }
}
