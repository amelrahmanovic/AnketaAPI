using System.ComponentModel.DataAnnotations;

namespace AnketaAPI.Models
{
    public class QuestionAnswer
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }

        public required int QuestionId { get; set; } 
        public Question Question { get; set;}

        public bool IsCorrect { get; set; } 
    }
}
