using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire.Business.Abstrack
{
    public interface IFactoryQuantityService
    {
        List<FactoryQuantity> GetMostReportByDate(DateTime firstDate, DateTime lastDate,int branchid);
        FactoryQuantity GetQuantityByDateAndFactoryid(int factoryid, DateTime date,int branchid);
        List<FactoryQuantity> GetQuantitiesByTypeid(int typeid, int branchid);

        List<FactoryQuantity> GetFactoryQuantitiesByFactoryid(int receiptId);
        List<FactoryQuantity> GetAll();
        FactoryQuantity GetById(int id);
        void Create(FactoryQuantity entity);
        void Delete(FactoryQuantity entity);
        void Update(FactoryQuantity entity);
    }
}
