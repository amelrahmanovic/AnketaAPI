namespace AnketaAPI.Models
{
    public class CatalogSurveyQuestion
    {
        public required int CatalogSurveyId { get; set; }

        public required int QuestionId { get; set; }

        public bool Answer { get; set; }
    }
}
