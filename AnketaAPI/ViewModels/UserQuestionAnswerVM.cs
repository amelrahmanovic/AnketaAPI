using AnketaAPI.Models;

namespace AnketaAPI.ViewModels
{
    public class UserQuestionAnswerVM
    {
        public int QuestionId { get; set; }
        public string? QuestionName { get; set; } = string.Empty;

        public List<QuestionAnswerUserVM> QuestionAnswerUserVM { get; set; }
    }
}
