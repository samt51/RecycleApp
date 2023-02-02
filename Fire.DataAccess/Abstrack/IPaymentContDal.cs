using Fire.Entity.Concrete;

namespace Fire.DataAccess.Abstrack
{
    public interface IPaymentContDal:IRepository<PaymentCont>
    {
        PaymentCont GetPaymentByPay(int receiptId);
    }
}
