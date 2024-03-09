using AnketaAPI.Models;

namespace AnketaAPI.ViewModels
{
    public class UserCatalogSurveryVM
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public bool Finished { get; set; }

        public int SuccessfulAnswers { get; set; } = 0;
        public int WrongAnswers { get; set; } = 0;

        public List<UserQuestionAnswerVM> UserQuestionAnswerVM { get; set; }
        
    }
}
