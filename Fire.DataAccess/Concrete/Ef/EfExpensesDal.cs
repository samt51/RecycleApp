using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfExpensesDal : EfRepositoryDal<Expenses, DataContext>, IExpensesDal
    {
        public List<Expenses> GetAllByBranchid(int branchid)
        {
            using (var db=new DataContext())
            {
                return db.Expenses.Where(x => x.branchid == branchid && x.İsDelete == false).ToList();
            }
        }

        public List<Expenses> GetAllExpensesWithDataTime(DateTime date,int branchid)
        {
            using (var db = new DataContext())
            {
                return db.Expenses.Where(x => x.CreatedDate.Year == date.Year && x.CreatedDate.Month == date.Month && x.CreatedDate.Day == date.Day&&x.İsDelete==false).ToList();
            }
        }
        public List<Expenses> MonthlyExpenses(int mounth, int branchid)
        {
            using (var db = new DataContext())
            {
                return db.Expenses.Where(x => x.CreatedDate.Month == mounth&&x.CreatedDate.Year==DateTime.Now.Year&&x.İsDelete == false).ToList();
            }
        }
        public List<Expenses> YearlyExpenses(int year, int branchid)
        {
            using (var db = new DataContext())
            {
                return db.Expenses.Where(x => x.CreatedDate.Year == year&& x.İsDelete == false).ToList();
            }
        }
    }
}