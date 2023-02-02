using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{
    public class CheckManager : ICheckService
    {
        private readonly ICheckDal _checkDal;
        public CheckManager(ICheckDal checkDal)
        {
            _checkDal = checkDal;
        }
        public void Create(Check entity)
        {
            _checkDal.Create(entity);
        }

        public void Delete(Check entity)
        {
            _checkDal.Delete(entity);
        }

        public List<Check> GetAll()
        {
            return _checkDal.GetAll();
        }

        public Check GetById(int id)
        {
            return _checkDal.GetById(id);
        }

        public List<Check> GetCheckByBranchid(int brancid)
        {
            return _checkDal.GetCheckByBranchid(brancid);
        }

        public Check GetCheckBySerialNumber(string serialnumber,int bankid)
        {
            return _checkDal.GetCheckBySerialNumber(serialnumber,bankid);
        }

        public List<Check> GetEmptyCheckByBranchId(int branchid)
        {
            return _checkDal.GetCheckByBranchid(branchid);
        }

        public List<Check> GetEmptyCheckByGiveNumber(int branchid, int giveid)
        {
            return _checkDal.GetEmptyCheckByGiveNumber(branchid, giveid);
        }

        public void Update(Check entity)
        {
            _checkDal.Update(entity);
        }
    }
}
