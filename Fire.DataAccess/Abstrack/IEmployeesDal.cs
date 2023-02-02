using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.DataAccess.Abstrack
{
    public interface IEmployeesDal:IRepository<Employees>
    {
        List<Employees> GetListEmployeesByBranchid(int brancid);
    }
}
