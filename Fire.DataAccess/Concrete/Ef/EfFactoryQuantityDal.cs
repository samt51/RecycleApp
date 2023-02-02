using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfFactoryQuantityDal : EfRepositoryDal<FactoryQuantity, DataContext>, IFactoryQuantityDal
    {
        
        public List<FactoryQuantity> GetFactoryQuantitiesByFactoryid(int receiptId)
        {
            using (var db = new DataContext())
            {

                return db.factoryQuantities.Where(x => x.ReceiptId == receiptId && x.İsDelete == false).ToList();
            }
        }

        public List<FactoryQuantity> GetMostReportByDate(DateTime firstDate, DateTime lastDate, int branchid)
        {
            using (var db = new DataContext())
            {
                if (branchid == 0)
                    return db.factoryQuantities.Where(x => x.CreatedDate >= firstDate && x.CreatedDate <= lastDate && x.İsDelete == false).ToList();
                else
                    return db.factoryQuantities.Where(x => x.CreatedDate >= firstDate && x.CreatedDate <= lastDate && x.id == branchid && x.İsDelete == false).ToList();

            }
        }

        public List<FactoryQuantity> GetQuantitiesByTypeid(int typeid, int branchid)
        {
            using (var db = new DataContext())
            {
                return db.factoryQuantities.Where(x => x.factoryproducttypeid == typeid && x.id == branchid && x.İsDelete == false).ToList();
            }
        }

        public FactoryQuantity GetQuantityByDateAndFactoryid(int factoryid, DateTime date, int branchid)
        {
            using (var db = new DataContext())
            {
                return db.factoryQuantities.Where(x => x.id == factoryid && x.CreatedDate == date && x.id == branchid && x.İsDelete == false).FirstOrDefault();
            }
        }
    }
}
