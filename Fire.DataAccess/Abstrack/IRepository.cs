using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fire.DataAccess.Abstrack
{
    public interface IRepository<T>
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        void AddMany(List<T> entitys);
        T GetById(int id);
        T Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
