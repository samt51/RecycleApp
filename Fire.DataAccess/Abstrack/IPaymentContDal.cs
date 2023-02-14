using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.DataAccess.Abstrack
{
    public interface IPaymentContDal : IRepository<PaymentCont>
    {
        PaymentCont GetPaymentByPay(int receiptId, bool IsWhat);
        List<PaymentCont> GetPaymentByIsWhat(bool IsWhat);
    }
}
