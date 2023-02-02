using Fire.Entity.Concrete;
using System.Collections.Generic;
namespace Fire.Business.Abstrack
{
    public interface IEmployeesService
    {
        List<Employees> GetListEmployeesByBranchid(int brancid);
        List<Employees> GetAll();
        Employees GetById(int id);
        void Create(Employees entity);
        void Delete(Employees entity);
        void Update(Employees entity);
    }
}
