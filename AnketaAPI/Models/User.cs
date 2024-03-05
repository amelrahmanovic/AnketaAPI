using System.ComponentModel.DataAnnotations;

namespace AnketaAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public required string Email { get; set; }
    }
}
