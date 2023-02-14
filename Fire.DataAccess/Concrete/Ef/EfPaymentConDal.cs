using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfPaymentConDal : EfRepositoryDal<PaymentCont, DataContext>, IPaymentContDal
    {
        public List<PaymentCont> GetPaymentByIsWhat(bool IsWhat)
        {
            using (var db = new DataContext())
            {
                return db.PaymentCont.Where(x => x.İsDelete == false && x.IsWhat == IsWhat).ToList();
            };
        }

        public PaymentCont GetPaymentByPay(int receiptId,bool IsWhat)
        {
            using (var db = new DataContext())
            {
                return db.PaymentCont.Where(x => x.ReceiptId == receiptId&&x.İsDelete==false&&x.IsWhat==IsWhat).FirstOrDefault();
            };
        }
    }
}
