using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{
    public class ProductTypeManager : IProductTypeService
    {
        private readonly IProductTypeDal _productTypeDal;
        public ProductTypeManager(IProductTypeDal productTypeDal)
        {
            _productTypeDal = productTypeDal;
        }

        public List<ProductType> AllProductTypeList()
        {
            return _productTypeDal.GetAll();
        }

        public ProductType Create(ProductType entity)
        {
          return  _productTypeDal.Create(entity);
        }

        public void Delete(ProductType entity)
        {
            _productTypeDal.Delete(entity);
        }

        public List<ProductType> GetAll()
        {
            return _productTypeDal.GetAll(x => x.İsDelete == false);
        }

        public List<ProductType> GetAllForFactory()
        {
            return _productTypeDal.GetAll(x => x.İsDelete == false && x.IsWhat == 2);
        }

        public ProductType GetById(int id)
        {
            return _productTypeDal.GetById(id);
        }

        public ProductType GetProductTypeByName(string name)
        {
            return _productTypeDal.GetProductTypeByName(name);
        }

        public void Update(ProductType entity)
        {
            _productTypeDal.Update(entity);
        }
    }
}
