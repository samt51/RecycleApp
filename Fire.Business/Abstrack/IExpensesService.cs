using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IExpensesService
    {
        List<Expenses> GetAllByBranchid(int branchid);
        List<Expenses> GetAllExpensesWithDataTime(DateTime date,int branchid);
        List<Expenses> YearlyExpenses(int year, int branchid);
        List<Expenses> MonthlyExpenses(int mounth, int branchid);
        List<Expenses> GetAll();
        Expenses GetById(int id);
        void Create(Expenses entity);
        void Delete(Expenses entity);
        void Update(Expenses entity);
    }
}
