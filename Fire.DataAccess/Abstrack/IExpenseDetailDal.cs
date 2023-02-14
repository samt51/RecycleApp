using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Fire.DataAccess.Abstrack
{
    public interface IExpenseDetailDal: IRepository<ExpenseDetail>
    {
        List<ExpenseDetail> GetExpenseByDateAndBranchId(DateTime startDate, DateTime endDate, int brancId);
    }
}
