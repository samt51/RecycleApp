using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Fire.DataAccess.Abstrack
{
    public interface IProductQuantityDal : IRepository<ProductQuantity>
    {
        List<ProductQuantity> GetProductQuantitiesByCustomerid(int customerid, int branchid);
        List<ProductQuantity> getQuntityesByids(int customerid, int quantity, DateTime date);
        List<ProductQuantity> GetQuantitiesByTypeid(int typeid);
        List<ProductQuantity> GetMostReportByDate(DateTime firstDate, DateTime lastDate, int branchid);
        List<ProductQuantity> GetQuantityByReceiptBy(int receiptId);
        List<ProductQuantity> GetQuantityBeetwenDate(DateTime startDate, DateTime lastDate);
    }
}
