using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire.DataAccess.Abstrack
{
    public interface IFactoryQuantityDal:IRepository<FactoryQuantity>
    {
        List<FactoryQuantity> GetFactoryQuantitiesByFactoryid(int receiptId);
        List<FactoryQuantity> GetQuantitiesByTypeid(int typeid, int branchid);
        FactoryQuantity GetQuantityByDateAndFactoryid(int factoryid, DateTime date,int branchid);
        List<FactoryQuantity> GetMostReportByDate(DateTime firstDate,DateTime lastDate,int branchid);
    }
}
