using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{
    public class ProductQuantityManager : IProductQuantityService
    {
        private readonly IProductQuantityDal _productCategoriaDal;
        public ProductQuantityManager(IProductQuantityDal productCategoriaDal)
        {
            _productCategoriaDal = productCategoriaDal;
        }
        public void Create(ProductQuantity entity)
        {
            _productCategoriaDal.Create(entity);
        }

        public void Delete(ProductQuantity entity)
        {
            _productCategoriaDal.Delete(entity);
        }

        public List<ProductQuantity> GetAll()
        {
            return _productCategoriaDal.GetAll();
        }

        public ProductQuantity GetById(int id)
        {
            return _productCategoriaDal.GetById(id);
        }



        public List<ProductQuantity> GetMostReportByDate(DateTime firstDate, DateTime lastDate, int branchid)
        {
            return _productCategoriaDal.GetMostReportByDate(firstDate, lastDate, branchid);
        }

        public List<ProductQuantity> GetProductQuantitiesByCustomerid(int customerid, int branchid)
        {
            return _productCategoriaDal.GetProductQuantitiesByCustomerid(customerid, branchid);
        }

        public List<ProductQuantity> GetQuantitiesByTypeid(int typeid)
        {
            return _productCategoriaDal.GetQuantitiesByTypeid(typeid);
        }

        public List<ProductQuantity> GetQuantityBeetwenDate( DateTime startDate, DateTime lastDate)
        {
            return _productCategoriaDal.GetQuantityBeetwenDate(startDate, lastDate);
        }

        public List<ProductQuantity> GetQuantityByReceiptBy(int receiptId)
        {
            return _productCategoriaDal.GetQuantityByReceiptBy(receiptId);
        }

        public List<ProductQuantity> getQuntityesByids(int customerid, int quantity, DateTime date)
        {
            return _productCategoriaDal.getQuntityesByids(customerid, quantity, date);
        }

        public void Update(ProductQuantity entity)
        {
            _productCategoriaDal.Update(entity);
        }
    }
}
