using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fire.Business.Concrete
{
    public class ExpenseDetailManager : IExpenseDetailService
    {
        private readonly IExpenseDetailDal _expenseDetailDal;
        public ExpenseDetailManager(IExpenseDetailDal expenseDetailDal)
        {
            _expenseDetailDal = expenseDetailDal;
        }

        public void Create(ExpenseDetail entity)
        {
            _expenseDetailDal.Create(entity);
        }

        public void Delete(ExpenseDetail entity)
        {
            throw new System.NotImplementedException();
        }

        public List<ExpenseDetail> GetAll(Expression<Func<ExpenseDetail, bool>> filter = null)
        {
            return _expenseDetailDal.GetAll(filter);
        }

        public ExpenseDetail GetById(int id)
        {
            return _expenseDetailDal.GetById(id);
        }

        public List<ExpenseDetail> GetExpenseByDateAndBranchId(DateTime startDate, DateTime endDate, int brancId)
        {
            return _expenseDetailDal.GetExpenseByDateAndBranchId(startDate, endDate, brancId);
        }

        public void Update(ExpenseDetail entity)
        {
            _expenseDetailDal.Update(entity);
        }
    }
}
