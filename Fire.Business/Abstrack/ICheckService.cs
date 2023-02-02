using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface ICheckService
    {
        Check GetCheckBySerialNumber(string serialnumber,int bankid );
        List<Check> GetEmptyCheckByGiveNumber(int branchid, int giveid);
        List<Check> GetCheckByBranchid(int brancid);
        List<Check> GetAll();
        Check GetById(int id);
        void Create(Check entity);
        void Delete(Check entity);
        void Update(Check entity);
        List<Check> GetEmptyCheckByBranchId(int branchid);
    }
}
