using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public void Create(Customer entity)
        {
            _customerDal.Create(entity);
        }

        public void Delete(Customer entity)
        {
            _customerDal.Create(entity);
        }

        public List<Customer> GetAll()
        {
            return _customerDal.GetAll();
        }

        public Customer GetById(int id)
        {
            return _customerDal.GetById(id);
        }

        public void Update(Customer entity)
        {
            _customerDal.Update(entity);
        }
    }
}
