namespace AnketaAPI.Models
{
    public class UserAnswer
    {
        public required int UserId { get; set; }
        public User? User { get; set; }

        public required int QuestionAnswerId { get; set; }
        public QuestionAnswer? QuestionAnswer { get; set; }

        public required bool Answer { get; set; }

    }
}
