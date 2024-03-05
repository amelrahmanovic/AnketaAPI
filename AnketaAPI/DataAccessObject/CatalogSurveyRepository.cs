using AnketaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnketaAPI.DataAccessObject
{
    public class CatalogSurveyRepository : IRepository<CatalogSurvey>
    {
        private readonly AppDbConext _context;
        public CatalogSurveyRepository(AppDbConext context)
        {
            _context = context;
        }
        public bool Add(CatalogSurvey catalogSurvey)
        {
            var result = _context.CatalogSurvey.Add(catalogSurvey);
            _context.SaveChanges();
            return result == null ? false : true;
        }

        public IEnumerable<CatalogSurvey> GetAll()
        {
            return _context.CatalogSurvey.Include(x=>x.CatalogSurveyQuestion).ToList();
        }

        public CatalogSurvey GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
