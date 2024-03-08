using AnketaAPI.Models;

namespace AnketaAPI.DataAccessObject.DAO
{
    public class UserCatalogSurveryDAO
    {
        AppDbConext context;
        public UserCatalogSurveryDAO(IConfiguration config)
        {
            context = new AppDbConext(config);
        }
        public bool Add(UserCatalogSurvery userCatalogSurvery)
        {
            var result = context.UserCatalogSurvery.Add(userCatalogSurvery);
            context.SaveChanges();
            return result == null ? false : true;
        }
    }
}
