using AnketaAPI.Models;

namespace AnketaAPI.DataAccessObject
{
    public class QuestionDAO
    {
        AppDbConext context;
        public QuestionDAO(IConfiguration config)
        {
            context = new AppDbConext(config);
        }
        public bool Add(Question question)
        {
            var result = context.Question.Add(question);
            context.SaveChanges();
            return result == null ? false : true;
        }
    }
}
