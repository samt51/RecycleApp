using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fire.Business.Concrete
{
    public class ExpenseCategoriaManager : IExpenseCategoriaService
    {
        private readonly IExpenseCategoriaDal _expenseCategoriaDal;
        public ExpenseCategoriaManager(IExpenseCategoriaDal expenseCategoriaDal)
        {
            _expenseCategoriaDal = expenseCategoriaDal;
        }

        public void Create(ExpenseCategoria entity)
        {
            _expenseCategoriaDal.Create(entity);
        }

        public void Delete(ExpenseCategoria entity)
        {
            throw new System.NotImplementedException();
        }

        public List<ExpenseCategoria> GetAll(Expression<Func<ExpenseCategoria, bool>> filter = null)
        {
            return _expenseCategoriaDal.GetAll(filter);
        }

        public ExpenseCategoria GetById(int id)
        {
            return _expenseCategoriaDal.GetById(id);
        }

        public void Update(ExpenseCategoria entity)
        {
            _expenseCategoriaDal.Update(entity);
        }
    }
}
