using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{
    public class FactoryProductTypeManager : IFactoryProductTypeService
    {
        private readonly IFactoryProductTypeDal _factoryProductTypeDal;
        public FactoryProductTypeManager(IFactoryProductTypeDal factoryProductTypeDal)
        {
            _factoryProductTypeDal = factoryProductTypeDal;
        }
        public void Create(FactoryProductType entity)
        {
            _factoryProductTypeDal.Create(entity);
        }

        public void Delete(FactoryProductType entity)
        {
            _factoryProductTypeDal.Delete(entity);
        }

        public List<FactoryProductType> GetAll()
        {
            return _factoryProductTypeDal.GetAll();
        }

        public FactoryProductType GetById(int id)
        {
            return _factoryProductTypeDal.GetById(id);
        }

        public void Update(FactoryProductType entity)
        {
            _factoryProductTypeDal.Update(entity);
        }
    }
}
