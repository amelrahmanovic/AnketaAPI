namespace AnketaAPI.ViewModels
{
    public class UserTestsVM
    {
        public int CatalogSurveyId { get; set; }
        public string CatalogSurveyName { get; set; } = "";
        public DateTime CatalogSurveyCreated { get; set; }

        public string TestDone { get; set; } = "";
        public int? UserId { get; set; }

        public List<UserQuestionAnswerVM>? userQuestionAnswerVMs { get;set; }
    }
}
