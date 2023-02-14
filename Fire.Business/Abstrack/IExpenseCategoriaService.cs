using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fire.Business.Abstrack
{
    public interface IExpenseCategoriaService
    {
        List<ExpenseCategoria> GetAll(Expression<Func<ExpenseCategoria, bool>> filter = null);
        ExpenseCategoria GetById(int id);
        void Create(ExpenseCategoria entity);
        void Delete(ExpenseCategoria entity);
        void Update(ExpenseCategoria entity);
    }
}
