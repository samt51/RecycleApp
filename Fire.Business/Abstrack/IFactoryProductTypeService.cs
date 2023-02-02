using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IFactoryProductTypeService
    {
        List<FactoryProductType> GetAll();
        FactoryProductType GetById(int id);
        void Create(FactoryProductType entity);
        void Delete(FactoryProductType entity);
        void Update(FactoryProductType entity);
    }
}
