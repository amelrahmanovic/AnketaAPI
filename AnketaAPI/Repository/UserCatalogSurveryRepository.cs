using AnketaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnketaAPI.Repository
{
    public class UserCatalogSurveryRepository : IRepository<UserCatalogSurvery>
    {
        private readonly AppDbConext _context;
        public UserCatalogSurveryRepository(AppDbConext context)
        {
            _context = context;
        }
        public bool Add(UserCatalogSurvery entity)
        {
            var userCatalogSurveryDB = _context.UserCatalogSurvery.SingleOrDefault(x => x.UserId == entity.UserId && x.CatalogSurveyId == entity.CatalogSurveyId);
            if (userCatalogSurveryDB != null)
                return true;

            var result = _context.UserCatalogSurvery.Add(entity);
            _context.SaveChanges();
            return result == null ? false : true;
        }

        public UserCatalogSurvery Add_Custom(UserCatalogSurvery entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(UserCatalogSurvery entity)
        {
            var userCatalogSurvery = _context.UserCatalogSurvery.SingleOrDefault(x=>x.UserId==entity.UserId && x.CatalogSurveyId==entity.CatalogSurveyId);
            if(userCatalogSurvery !=null)
            {
                userCatalogSurvery.Finished = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id1, int id2)
        {
            var userCatalogSurvery = _context.UserCatalogSurvery.SingleOrDefault(x => x.UserId == id1 && x.CatalogSurveyId == id2);
            if(userCatalogSurvery == null) return false;
            var result = _context.UserCatalogSurvery.Remove(userCatalogSurvery);
            _context.SaveChanges();
            return result == null ? false : true;
        }

        public IEnumerable<UserCatalogSurvery> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserCatalogSurvery GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserCatalogSurvery> GetById_Custom(int id)
        {
            return _context.UserCatalogSurvery.Include(x=>x.User).Where(x => x.CatalogSurveyId == id).ToList();
        }
        public IEnumerable<UserCatalogSurvery> GetById_Custom4(int id)
        {
            return _context.UserCatalogSurvery
                .Include(x => x.CatalogSurvey)
                .Include(y=>y.User)
                .Include(o=>o.CatalogSurvey.CatalogSurveyQuestion)
                .Where(z=>z.UserId==id && z.Finished==false).ToList();
        }

        public List<Question> GetById_Custom2(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public UserCatalogSurvery GetById_Custom3(int v1, int v2)
        {
            throw new NotImplementedException();
        }
    }
}
