using AnketaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AnketaAPI.DataAccessObject
{
    public class CatalogSurveyQuestionRepository : IRepository<CatalogSurveyQuestion>
    {
        private readonly AppDbConext _context;
        public CatalogSurveyQuestionRepository(AppDbConext context)
        {
             _context = context;
        }
        public bool Add(CatalogSurveyQuestion entity)
        {
            var result = _context.CatalogSurveyQuestion.Add(entity);
            _context.SaveChanges();
            return result == null ? false : true;
        }

        public CatalogSurveyQuestion Add_Custom(CatalogSurveyQuestion entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(CatalogSurveyQuestion entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id1, int id2)
        {
            var catalogSurveyQuestion = _context.CatalogSurveyQuestion.SingleOrDefault(x => x.QuestionId == id1 && x.CatalogSurveyId == id2);
            if (catalogSurveyQuestion != null)
            {
                _context.CatalogSurveyQuestion.Remove(catalogSurveyQuestion);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<CatalogSurveyQuestion> GetAll()
        {
            throw new NotImplementedException();
        }

        public CatalogSurveyQuestion GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CatalogSurveyQuestion> GetById_Custom(int id)
        {
            return _context.CatalogSurveyQuestion.Where(x => x.CatalogSurveyId == id).ToList();
        }

        public List<Question> GetById_Custom2(List<int> ids)
        {
            throw new NotImplementedException();
        }

        //        public IEnumerable<CatalogSurveyQuestion> GetById_Custom2(int id)
        //        {
        //#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        //            IEnumerable<CatalogSurveyQuestion> res = _context.CatalogSurveyQuestion.Where(x => x.CatalogSurveyId == id).Include(x => x.Question).ToList();
        //#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        //            return res;
        //            //return _context.CatalogSurveyQuestion.Where(x => x.CatalogSurveyId == id).Include(x=>x.Question).ToList();
        //        }

    }
}
