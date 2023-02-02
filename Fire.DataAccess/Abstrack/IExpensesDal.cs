using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Fire.DataAccess.Abstrack
{
    public interface IExpensesDal:IRepository<Expenses>
    {
        List<Expenses> GetAllByBranchid(int branchid);
        public List<Expenses> GetAllExpensesWithDataTime(DateTime date,int branchid);
        public List<Expenses> MonthlyExpenses(int mounth,int branchid);
        public List<Expenses> YearlyExpenses(int year,int branchid);
    }
}
