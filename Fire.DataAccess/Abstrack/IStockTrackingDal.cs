using Fire.Entity.Concrete;

namespace Fire.DataAccess.Abstrack
{
    public interface IStockTrackingDal:IRepository<StockTracking>
    {
        public StockTracking GetStockByProductId(int productid,int branchid);
    }
}
