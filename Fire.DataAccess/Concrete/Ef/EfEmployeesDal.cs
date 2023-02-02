using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfEmployeesDal : EfRepositoryDal<Employees, DataContext>, IEmployeesDal
    {
        public List<Employees> GetListEmployeesByBranchid(int brancid)
        {
            using (var db = new DataContext())
            {
                return db.Employees.Where(x => x.branchid == brancid&&x.İsDelete==false).ToList();
            }
        }
    }
}
