using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire.Business.Concrete
{
    public class FactoryQuantityManager : IFactoryQuantityService
    {
        private readonly IFactoryQuantityDal _factoryQuantityDal;
        public FactoryQuantityManager(IFactoryQuantityDal factoryQuantityDal)
        {
            _factoryQuantityDal = factoryQuantityDal;
        }
        public void Create(FactoryQuantity entity)
        {
            _factoryQuantityDal.Create(entity);
        }

        public void Delete(FactoryQuantity entity)
        {
            _factoryQuantityDal.Delete(entity);
        }

        public List<FactoryQuantity> GetAll()
        {
            return _factoryQuantityDal.GetAll();
        }

        public FactoryQuantity GetById(int id)
        {
            return _factoryQuantityDal.GetById(id);
        }

      

        public List<FactoryQuantity> GetFactoryQuantitiesByFactoryid(int receiptId)
        {
            return _factoryQuantityDal.GetFactoryQuantitiesByFactoryid(receiptId);
        }

        public List<FactoryQuantity> GetMostReportByDate(DateTime firstDate, DateTime lastDate,int branchid)
        {
            return _factoryQuantityDal.GetMostReportByDate(firstDate, lastDate,branchid);
        }

        public List<FactoryQuantity> GetQuantitiesByTypeid(int typeid,int branchid)
        {
            return _factoryQuantityDal.GetQuantitiesByTypeid(typeid,branchid);
        }

        public FactoryQuantity GetQuantityByDateAndFactoryid(int factoryid, DateTime date,int branchid)
        {
            return _factoryQuantityDal.GetQuantityByDateAndFactoryid(factoryid, date,branchid);
        }

        public void Update(FactoryQuantity entity)
        {
            _factoryQuantityDal.Update(entity);
        }
    }
}
