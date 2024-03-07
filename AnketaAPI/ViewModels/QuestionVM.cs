using AnketaAPI.Models;

namespace AnketaAPI.ViewModels
{
    public class QuestionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public IList<CatalogSurveyQuestion> CatalogSurveyQuestion { get; set; }
        public IList<QuestionAnswerVM> QuestionAnswer { get; set; }
    }
}
