using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fire.Business.Abstrack
{
    public interface IExpenseDetailService
    {
        List<ExpenseDetail> GetAll(Expression<Func<ExpenseDetail, bool>> filter = null);
        ExpenseDetail GetById(int id);
        void Create(ExpenseDetail entity);
        void Delete(ExpenseDetail entity);
        void Update(ExpenseDetail entity);
        List<ExpenseDetail> GetExpenseByDateAndBranchId(DateTime startDate, DateTime endDate, int brancId);
    }
}
