using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using System.Linq;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfStockTrackingDal : EfRepositoryDal<StockTracking, DataContext>, IStockTrackingDal
    {
        public StockTracking GetStockByProductId(int productid,int branchid)
        {
            using (var db=new DataContext())
            {
                return db.StockTracking.Where(x => x.productid == productid&&x.branchid==branchid&&x.İsDelete==false).FirstOrDefault();
            }
        }
    }
}
