using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using System.Linq;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfPaymentConDal : EfRepositoryDal<PaymentCont, DataContext>, IPaymentContDal
    {
        public PaymentCont GetPaymentByPay(int receiptId)
        {
            using (var db = new DataContext())
            {
                return db.PaymentCont.Where(x => x.ReceiptId == receiptId&&x.İsDelete==false).FirstOrDefault();
            };
        }
    }
}
