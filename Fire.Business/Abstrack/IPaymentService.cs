
using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IPaymentService
    {
        List<Payment> GetAll();
        Payment GetById(int id);
        void Create(Payment entity);
        void Delete(Payment entity);
        void Update(Payment entity);
    }
}
