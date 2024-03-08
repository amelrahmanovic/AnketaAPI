using AnketaAPI.Models;

namespace AnketaAPI.DataAccessObject
{
    public class CatalogSurveyDAO
    {
        AppDbConext context;
        public CatalogSurveyDAO(IConfiguration config)
        {
            context = new AppDbConext(config);
        }
        public bool Add(CatalogSurvey catalogSurvey)
        {
            var result = context.CatalogSurvey.Add(catalogSurvey);
            context.SaveChanges();
            return result == null ? false : true;
        }
        public CatalogSurvey Get(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return context.CatalogSurvey.SingleOrDefault(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
