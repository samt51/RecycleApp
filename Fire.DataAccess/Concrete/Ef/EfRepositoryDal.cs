using Fire.DataAccess.Abstrack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfRepositoryDal<T, TContext> : IRepository<T> where T : class where TContext : DbContext, new()
    {
        public void AddMany(List<T> entitys)
        {
            using (var db = new TContext())
            {
                db.Set<T>().AddRange(entitys);
                db.SaveChanges();
            }
        }

        public T Create(T entity)
        {
            using (var db = new TContext())
            {
                var data = db.Set<T>().Add(entity);
                db.SaveChanges();
                return data.Entity;


            }
        }
        public void Delete(T entity)
        {
            using (var db = new TContext())
            {
                db.Set<T>().Remove(entity);
                db.SaveChanges();
            }
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            using (var db = new TContext())
            {
                return filter == null
                  ? db.Set<T>().ToList()
                  : db.Set<T>().Where(filter).ToList();
            }
        }

        public T GetById(int id)
        {
            using (var db = new TContext())
            {
                return db.Set<T>().Find(id);

            }
        }

        public void Update(T entity)
        {
            using (var db = new TContext())
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
