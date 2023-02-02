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
    public class EfProductQuantityDal : EfRepositoryDal<ProductQuantity, DataContext>, IProductQuantityDal
    {
        public List<ProductQuantity> GetMostReportByDate(DateTime firstDate, DateTime lastDate, int branchid)
        {
            using (var db = new DataContext())
            {
                if (branchid == 0)
                {
                    return db.productQuantity.Where(x => x.CreatedDate >= firstDate && x.CreatedDate <= lastDate && x.İsDelete == false).ToList();
                }
                return db.productQuantity.Where(x => x.CreatedDate >= firstDate && x.CreatedDate <= lastDate && x.id == branchid && x.İsDelete == false).ToList();
            }
        }

        public List<ProductQuantity> GetProductQuantitiesByCustomerid(int customerid, int branchid)
        {
            using (var db = new DataContext())
            {
                if (branchid == 0)
                    return db.productQuantity.ToList();
                else
                    return db.productQuantity.Where(x => x.id == customerid && x.id == branchid).ToList();
            }
        }

        public List<ProductQuantity> GetQuantitiesByTypeid(int typeid)
        {
            using (var db = new DataContext())
            {
                return db.productQuantity.Where(x => x.productTypeid == typeid).ToList();
            }
        }

        public List<ProductQuantity> GetQuantityBeetwenDate(DateTime startDate, DateTime lastDate)
        {
            using (var db = new DataContext())
            {
                var query = db.productQuantity.Where(x => x.CreatedDate >= startDate && x.CreatedDate <= lastDate && x.İsDelete == false).ToList();
                return query;
            }
        }

        public List<ProductQuantity> GetQuantityByReceiptBy(int receiptId)
        {
            using (var db = new DataContext())
            {
                var query = db.productQuantity.Where(x => x.ReceiptId == receiptId && x.İsDelete == false).ToList();
                return query;
            }
        }

        public List<ProductQuantity> getQuntityesByids(int customerid, int quantity, DateTime date)
        {
            using (var db = new DataContext())
            {
                return db.productQuantity.Where(x => x.id == customerid && x.id == quantity && x.CreatedDate == date).ToList();
            }
        }
    }
}
