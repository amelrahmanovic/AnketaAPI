using AnketaAPI.Models;
using AnketaAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AnketaAPI.Repository
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

        public CatalogSurvey Add_Custom(CatalogSurvey entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(CatalogSurvey catalogSurvey)
        {
            var result = _context.CatalogSurvey.Remove(catalogSurvey);
            _context.SaveChanges();
            return result == null ? false : true;
        }

        public bool Delete(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CatalogSurvey> GetAll()
        {
            return _context.CatalogSurvey.Include(x => x.CatalogSurveyQuestion).Include(x => x.UserCatalogSurvery).AsNoTracking().ToList();
        }

        public CatalogSurvey GetById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.CatalogSurvey.AsNoTracking().SingleOrDefault(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public IEnumerable<CatalogSurvey> GetById_Custom(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CatalogSurveyQuestion> GetById_Custom2(int id)
        {
            throw new NotImplementedException();
        }

        public List<Question> GetById_Custom2(List<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
