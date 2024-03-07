namespace AnketaAPI.Models
{
    public class CatalogSurveyQuestion
    {
        public required int CatalogSurveyId { get; set; }
        public CatalogSurvey? CatalogSurvey { get; set; }

        public required int QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
