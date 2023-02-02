using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire.Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private readonly IPaymentDal _paymentDal;
        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }
        public void Create(Payment entity)
        {
            _paymentDal.Create(entity);
        }

        public void Delete(Payment entity)
        {
            _paymentDal.Delete(entity);
        }

        public List<Payment> GetAll()
        {
            return _paymentDal.GetAll();
        }

        public Payment GetById(int id)
        {
            return _paymentDal.GetById(id);
        }

        public void Update(Payment entity)
        {
            _paymentDal.Update(entity);
        }
    }
}
