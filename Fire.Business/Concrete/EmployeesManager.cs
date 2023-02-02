using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{
    public class EmployeesManager : IEmployeesService
    {
        private readonly IEmployeesDal _employeesDal;
        public EmployeesManager(IEmployeesDal employeesDal)
        {
            _employeesDal = employeesDal;
        }
        public void Create(Employees entity)
        {
            _employeesDal.Create(entity);
        }

        public void Delete(Employees entity)
        {
            _employeesDal.Delete(entity);
        }

        public List<Employees> GetAll()
        {
            return _employeesDal.GetAll();
        }

        public Employees GetById(int id)
        {
            return _employeesDal.GetById(id);
        }

        public List<Employees> GetListEmployeesByBranchid(int brancid)
        {
            if (brancid == 0)
                return _employeesDal.GetAll();
            else
                return _employeesDal.GetListEmployeesByBranchid(brancid);
        }

        public void Update(Employees entity)
        {
            _employeesDal.Update(entity);
        }
    }
}
