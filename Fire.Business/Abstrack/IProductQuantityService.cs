using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IProductQuantityService
    {

        List<ProductQuantity> GetMostReportByDate(DateTime firstDate, DateTime lastDate,int branchid);
        List<ProductQuantity> GetQuantitiesByTypeid(int typeid);
        List<ProductQuantity> getQuntityesByids(int customerid, int quantity, DateTime date);
        List<ProductQuantity> GetProductQuantitiesByCustomerid(int customerid,int branchid);
        List<ProductQuantity> GetAll();
        List<ProductQuantity> GetQuantityByReceiptBy(int receiptId);
        List<ProductQuantity> GetQuantityBeetwenDate( DateTime startDate, DateTime lastDate);
        ProductQuantity GetById(int id);
        void Create(ProductQuantity entity);
        void Delete(ProductQuantity entity);
        void Update(ProductQuantity entity);
    }
}
