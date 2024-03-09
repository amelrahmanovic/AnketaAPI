using AnketaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnketaAPI.Repository
{
    public class QuestionAnswerRepository : IRepository<QuestionAnswer>
    {
        private readonly AppDbConext _context;
        public QuestionAnswerRepository(AppDbConext context)
        {
            _context = context;
        }

        public bool Add(QuestionAnswer entity)
        {
            var result = _context.QuestionAnswer.Add(entity);
            _context.SaveChanges();
            return result == null ? false : true;
        }

        public QuestionAnswer Add_Custom(QuestionAnswer entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(QuestionAnswer entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id1, int id2)
        {
            QuestionAnswer? questionAnswer = GetById(id1);
            if (questionAnswer != null)
            {
                var result = _context.QuestionAnswer.Remove(questionAnswer);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<QuestionAnswer> GetAll()
        {
            throw new NotImplementedException();
        }

        public QuestionAnswer GetById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.QuestionAnswer.SingleOrDefault(x => x.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public IEnumerable<QuestionAnswer> GetById_Custom(int id)
        {
            return _context.QuestionAnswer.Where(x => x.QuestionId == id).ToList();
        }

        public List<Question> GetById_Custom2(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public QuestionAnswer GetById_Custom3(int v1, int v2)
        {
            throw new NotImplementedException();
        }
    }
}
