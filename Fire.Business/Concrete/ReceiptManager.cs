using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.DataAccess.Models.DashboardReportModel;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{
    public class ReceiptManager : IReceiptService
    {
        private readonly IReceiptDal _receiptDal;
        public ReceiptManager(IReceiptDal receiptDal)
        {
            _receiptDal = receiptDal;
        }

        public Receipt Create(Receipt entity)
        {
            return _receiptDal.Create(entity);
        }

        public DashboardViewModel dashboard(int companyid, int branchid)
        {
            return _receiptDal.dashboard(companyid, branchid);
        }

        public void Delete(Receipt entity)
        {
            _receiptDal.Delete(entity);
        }



        public Receipt GetById(int id)
        {
            return _receiptDal.GetById(id);
        }

        public List<Receipt> GetReceiptBeetwenDate(DateTime startDate, int branchid, bool IsWhat, DateTime endDate)
        {
            return _receiptDal.GetReceiptBeetwenDate(startDate, branchid, IsWhat, endDate);
        }

        public List<Receipt> GetReceiptByCustomerId(int customerid, int branchId, bool IsWhat)
        {
            return _receiptDal.GetReceiptByCustomerId(customerid, branchId, IsWhat);
        }

        public List<Receipt> GetReceiptsByDateAndBranchId(DateTime date, int branchid, bool IsWhat)
        {
            return _receiptDal.GetReceiptsByDateAndBranchId(date, branchid, IsWhat);
        }

        public void Update(Receipt entity)
        {
            _receiptDal.Update(entity);
        }

    }
}
