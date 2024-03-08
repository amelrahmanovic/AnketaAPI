using AnketaAPI.Models;

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
            throw new NotImplementedException();
        }

        public bool Delete(int id1, int id2)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public List<Question> GetById_Custom2(List<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
