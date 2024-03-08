using AnketaAPI.Models;

namespace AnketaAPI.DataAccessObject.DAO
{
    public class QuestionAnswerDAO
    {
        AppDbConext context;
        public QuestionAnswerDAO(IConfiguration config)
        {
            context = new AppDbConext(config);
        }
        public bool Add(QuestionAnswer questionAnswer)
        {
            var result = context.QuestionAnswer.Add(questionAnswer);
            context.SaveChanges();
            return result == null ? false : true;
        }
    }
}
