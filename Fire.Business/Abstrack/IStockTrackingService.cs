using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IStockTrackingService
    {
        StockTracking GetStockByProductId(int productid, int branchid);
        List<StockTracking> GetAll();
        StockTracking GetById(int id);
        void Create(StockTracking entity);
        void Delete(StockTracking entity);
        void Update(StockTracking entity);
    }
}
