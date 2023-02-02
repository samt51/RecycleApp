using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{
    public class BankManager : IBankService
    {
        private readonly IBankDal _bankDal;
        public BankManager(IBankDal bankDal)
        {
            _bankDal = bankDal;
        }
        public void Create(bank entity)
        {
            _bankDal.Create(entity);
        }

        public void Delete(bank entity)
        {
            _bankDal.Delete(entity);
        }

        public List<bank> GetAll()
        {
            return _bankDal.GetAll(x=>x.İsDelete==false);
        }

        public bank GetById(int id)
        {
            return _bankDal.GetById(id);
        }

        public void Update(bank entity)
        {
            _bankDal.Update(entity);
        }
    }
}
