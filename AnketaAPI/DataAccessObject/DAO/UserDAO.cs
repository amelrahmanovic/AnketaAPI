using AnketaAPI.Models;

namespace AnketaAPI.DataAccessObject.DAO
{
    public class UserDAO
    {
        IConfiguration config;
        AppDbConext context;
        public UserDAO(IConfiguration config)
        {
            this.config = config;
            context = new AppDbConext(this.config);
        }
        public bool Add(User user)
        {
            var result = context.User.Add(user);
            context.SaveChanges();
            return result == null ? false : true;
        }
        public User Get(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return context.User.SingleOrDefault(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
