using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface ICustomerService
    {
        List<Customer> GetAll();
        Customer GetById(int id);
        void Create(Customer entity);
        void Delete(Customer entity);
        void Update(Customer entity);
    }
}
