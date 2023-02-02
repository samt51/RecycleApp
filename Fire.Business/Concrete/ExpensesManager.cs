using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{
    public class ExpensesManager : IExpensesService
    {
        private readonly IExpensesDal _expensesDal;
        public ExpensesManager(IExpensesDal expensesDal)
        {
            _expensesDal = expensesDal;
        }
        public void Create(Expenses entity)
        {
            _expensesDal.Create(entity);
        }

        public void Delete(Expenses entity)
        {
            _expensesDal.Delete(entity);
        }

        public List<Expenses> GetAll()
        {
           return _expensesDal.GetAll( );
        }

        public List<Expenses> GetAllByBranchid(int branchid)
        {
            if (branchid == 0)
                return _expensesDal.GetAll();
            else
                return _expensesDal.GetAllByBranchid(branchid);
        }

        public List<Expenses> GetAllExpensesWithDataTime(DateTime date, int branchid)
        {
            return _expensesDal.GetAllExpensesWithDataTime(date,branchid);
        }

        public Expenses GetById(int id)
        {
            return _expensesDal.GetById(id);
        }

        public List<Expenses> MonthlyExpenses(int mounth, int branchid)
        {
            return _expensesDal.MonthlyExpenses(mounth,branchid);
        }

        public void Update(Expenses entity)
        {
            _expensesDal.Update(entity);
        }

        public List<Expenses> YearlyExpenses(int year, int branchid)
        {
            return _expensesDal.YearlyExpenses(year,branchid);
        }
    }
}
