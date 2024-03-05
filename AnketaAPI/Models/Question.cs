using System.ComponentModel.DataAnnotations;

namespace AnketaAPI.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public required string Name {get;set;}

        public IList<CatalogSurveyQuestion> CatalogSurveyQuestion { get; set; }
    }
}
