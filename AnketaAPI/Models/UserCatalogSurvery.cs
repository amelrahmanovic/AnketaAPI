namespace AnketaAPI.Models
{
    public class UserCatalogSurvery
    {
        public required int UserId { get; set; }
        public User? User { get; set; }

        public required int CatalogSurveyId { get; set; }
        public CatalogSurvey? CatalogSurvey { get; set; }

        public bool Finished { get; set; }
    }
}
