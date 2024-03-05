using AnketaAPI.Models;

namespace AnketaAPI.DataAccessObject
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        bool Add(T entity);
        //bool Update(T entity);
        //bool Delete(T entity);
    }
}
