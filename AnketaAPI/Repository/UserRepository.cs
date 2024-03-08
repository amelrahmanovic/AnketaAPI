using AnketaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnketaAPI.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbConext _context;
        public UserRepository(AppDbConext context)
        {
            _context = context;
        }

        public bool Add(User user)
        {
            var userFind = _context.User.SingleOrDefault(x => x.Email == user.Email);
            if (userFind == null)
                return false;
            var result = _context.User.Add(user);
            _context.SaveChanges();
            return result == null ? false : true;
        }

        public User Add_Custom(User entity)
        {
            var userFind = _context.User.SingleOrDefault(x => x.Email == entity.Email);
            if (userFind != null)
                return userFind;

            var result = _context.User.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.User.ToList();
        }

        public User GetById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.User.SingleOrDefault(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public IEnumerable<User> GetById_Custom(int id)
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
