using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.DataAccess.Abstrack
{
    public interface ICheckDal:IRepository<Check>
    {
        List<Check> GetCheckByBranchid(int brancid);
        List<Check> GetEmptyCheckByGiveNumber(int branchid, int giveid);
        Check GetCheckBySerialNumber(string serialnumber,int bankid);
        List<Check> GetEmptyCheckByBranchId(int branchid);
    }
}
