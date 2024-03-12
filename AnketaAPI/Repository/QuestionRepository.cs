using AnketaAPI.Models;

namespace AnketaAPI.Repository
{
    public class QuestionRepository : IRepository<Question>
    {
        private readonly AppDbConext _context;
        public QuestionRepository(AppDbConext context)
        {
            _context = context;
        }
        public bool Add(Question entity)
        {
            throw new NotImplementedException();
        }

        public Question Add_Custom(Question question)
        {
            var result = _context.Question.Add(question);
            _context.SaveChanges();
            return question;
        }

        public bool Delete(Question entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetAll()
        {
            throw new NotImplementedException();
        }

        public Question GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetById_Custom(int id)
        {
            throw new NotImplementedException();
        }

        public Question GetById_Custom3(int v1, int v2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetById_Custom4(int id)
        {
            throw new NotImplementedException();
        }

        List<Question> IRepository<Question>.GetById_Custom2(List<int> ids)
        {
            return _context.Question.Where(q => ids.Contains(q.Id)).ToList();
        }
    }
}
