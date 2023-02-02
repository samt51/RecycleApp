using Fire.DataAccess.Models.DashboardReportModel;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IReceiptService
    {
        List<Receipt> GetReceiptByCustomerId(int customerid, int branchId, bool IsWhat);
        List<Receipt> GetReceiptsByDateAndBranchId(DateTime date, int branchid, bool IsWhat);
        List<Receipt> GetReceiptBeetwenDate(DateTime startDate, int branchid, bool IsWhat, DateTime endDate);
        Receipt GetById(int id);
        Receipt Create(Receipt entity);
        void Delete(Receipt entity);
        void Update(Receipt entity);
        DashboardViewModel dashboard(int companyid, int branchid);
    }
}
