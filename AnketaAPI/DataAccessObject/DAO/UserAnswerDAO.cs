using AnketaAPI.Models;

namespace AnketaAPI.DataAccessObject.DAO
{
    public class UserAnswerDAO
    {
        AppDbConext context;
        public UserAnswerDAO(IConfiguration config)
        {
            context = new AppDbConext(config);
        }
        public bool Add(UserAnswer userAnswer)
        {
            var result = context.UserAnswer.Add(userAnswer);
            context.SaveChanges();
            return result == null ? false : true;
        }
    }
}
