using System.ComponentModel.DataAnnotations;

namespace AnketaAPI.Models
{
    public class CatalogSurvey
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }

        public IList<CatalogSurveyQuestion> CatalogSurveyQuestion { get; set; }
    }
}
