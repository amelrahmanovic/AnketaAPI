﻿using AnketaAPI.Models;

namespace AnketaAPI.Repository
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetById_Custom(int id);
        IEnumerable<T> GetById_Custom4(int id);
        IEnumerable<T> GetAll();
        bool Add(T entity);
        T Add_Custom(T entity);
        //bool Update(T entity);
        bool Delete(T entity);
        bool Delete(int id1, int id2);
        List<Question> GetById_Custom2(List<int> ids);
        T GetById_Custom3(int v1, int v2);
    }
}
