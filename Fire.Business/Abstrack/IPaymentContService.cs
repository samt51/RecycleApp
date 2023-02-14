using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IPaymentContService
    {
        List<PaymentCont> GetAll();
        PaymentCont GetById(int id);
        PaymentCont Create(PaymentCont entity);
        void Delete(PaymentCont entity);
        void Update(PaymentCont entity);
        PaymentCont GetPaymentByPay(int receiptId, bool IsWhat);
        List<PaymentCont> GetPaymentByIsWhat(bool IsWhat);
    }
}
