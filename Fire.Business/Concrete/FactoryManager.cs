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
    public class FactoryManager : IFactoryService
    {
        private readonly IFactoryDal _factoryDal;
        public FactoryManager(IFactoryDal factoryDal)
        {
            _factoryDal = factoryDal;
        }
        public void Create(Factory entity)
        {
            _factoryDal.Create(entity);
        }

        public void Delete(Factory entity)
        {
            _factoryDal.Delete(entity);
        }

        public List<Factory> GetAll()
        {
            return _factoryDal.GetAll();
        }

        public Factory GetById(int id)
        {
            return _factoryDal.GetById(id);
        }

        public void Update(Factory entity)
        {
            _factoryDal.Update(entity);
        }
    }
}
