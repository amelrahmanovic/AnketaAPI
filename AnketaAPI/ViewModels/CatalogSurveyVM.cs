using AnketaAPI.Models;

namespace AnketaAPI.ViewModels
{
    public class CatalogSurveyVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public int Questions { get; set; }
        public int Users { get; set; }

        //public IList<CatalogSurveyQuestion> CatalogSurveyQuestion { get; set; }
    }
}
