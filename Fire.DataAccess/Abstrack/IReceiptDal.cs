using Fire.DataAccess.Models.DashboardReportModel;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Fire.DataAccess.Abstrack
{
    public interface IReceiptDal : IRepository<Receipt>
    {
        List<Receipt> GetReceiptByCustomerId(int customerid, int branchId, bool IsWhat);
        List<Receipt> GetReceiptsByDateAndBranchId(DateTime date, int branchid, bool IsWhat);
        DashboardViewModel dashboard(int companyid, int branchid);
        List<Receipt> GetReceiptBeetwenDate(DateTime startDate, int branchid, bool IsWhat, DateTime endDate);

    }
}
