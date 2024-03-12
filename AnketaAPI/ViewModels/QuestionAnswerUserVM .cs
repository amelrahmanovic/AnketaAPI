namespace AnketaAPI.ViewModels
{
    public class QuestionAnswerUserVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsCorrect { get; set; }

        public bool UserAnswer { get; set; }
    }
}
