using AnketaAPI.Models;

namespace AnketaAPI.Repository
{
    public class UserAnswerRepository : IRepository<UserAnswer>
    {
        private readonly AppDbConext _context;
        public UserAnswerRepository(AppDbConext context)
        {
            _context = context;
        }

        public bool Add(UserAnswer entity)
        {
            var result = _context.UserAnswer.Add(entity);
            _context.SaveChanges();
            return result == null ? false : true;
        }

        public UserAnswer Add_Custom(UserAnswer entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(UserAnswer entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserAnswer> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserAnswer GetById(int id)
        {
            throw new NotImplementedException();
        }
        public UserAnswer? GetById_Custom3(int id1, int id2)
        {
            return _context.UserAnswer.SingleOrDefault(x => x.UserId == id1 && x.QuestionAnswerId == id2);
        }

        public IEnumerable<UserAnswer> GetById_Custom(int id)
        {
            throw new NotImplementedException();
        }

        public List<Question> GetById_Custom2(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserAnswer> GetById_Custom4(int id)
        {
            throw new NotImplementedException();
        }
    }
}
