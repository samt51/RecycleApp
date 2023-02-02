using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IProductTypeService
    {
        ProductType GetProductTypeByName(string name);
        List<ProductType> GetAll();
        List<ProductType> GetAllForFactory();
        ProductType GetById(int id);
        void Create(ProductType entity);
        void Delete(ProductType entity);
        void Update(ProductType entity);
        List<ProductType> AllProductTypeList();

    }
}
