using AnketaAPI.Models;

namespace AnketaAPI.DataAccessObject
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetById_Custom(int id);
        IEnumerable<T> GetAll();
        bool Add(T entity);
        T Add_Custom(T entity);
        //bool Update(T entity);
        bool Delete(T entity);
        bool Delete(int id1, int id2);
        List<Question> GetById_Custom2(List<int> ids);
    }
}
