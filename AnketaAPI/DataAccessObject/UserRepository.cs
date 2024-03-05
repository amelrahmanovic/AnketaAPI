using AnketaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnketaAPI.DataAccessObject
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
            var result = _context.User.Add(user);
            _context.SaveChanges();
            return result == null ? false : true;
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
    }
}
