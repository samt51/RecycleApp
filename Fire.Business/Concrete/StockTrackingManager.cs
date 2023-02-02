using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{
    public class StockTrackingManager : IStockTrackingService
    {
        private readonly IStockTrackingDal _stockTrackingDal;
        public StockTrackingManager(IStockTrackingDal stockTrackingDal)
        {
            _stockTrackingDal = stockTrackingDal;
        }
        public void Create(StockTracking entity)
        {
            _stockTrackingDal.Create(entity);
        }

        public void Delete(StockTracking entity)
        {
            _stockTrackingDal.Delete(entity);
        }

        public List<StockTracking> GetAll()
        {
            return _stockTrackingDal.GetAll();
        }

        public StockTracking GetById(int id)
        {
            return _stockTrackingDal.GetById(id);
        }

        public StockTracking GetStockByProductId(int productid, int branchid)
        {
            return _stockTrackingDal.GetStockByProductId(productid, branchid);
        }
        public void Update(StockTracking entity)
        {
            _stockTrackingDal.Update(entity);
        }
    }
}
