using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfExpenseDetailDal : EfRepositoryDal<ExpenseDetail, DataContext>, IExpenseDetailDal
    {
        public List<ExpenseDetail> GetExpenseByDateAndBranchId(DateTime startDate, DateTime endDate, int brancId)
        {
            using (var db = new DataContext())
            {
                return db.expenseDetails.Where(x => x.BranchId == brancId && x.CreatedDate >= startDate && x.CreatedDate <= endDate).ToList();
            }
        }
    }
}
