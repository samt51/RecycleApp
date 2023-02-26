using Fire.Business.Abstrack;
using Fire.DataAccess.Abstrack;
using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Concrete
{

    public class ProductPricesManager : IProductPricesService
    {
        private readonly IProductPricesDal _productPrices;
        public ProductPricesManager(IProductPricesDal productPrices)
        {
            _productPrices = productPrices;
        }

        public void Create(ProductPrices entity)
        {
            _productPrices.Create(entity);
        }

        public void Delete(ProductPrices entity)
        {
            _productPrices.Delete(entity);
        }

        public List<ProductPrices> GetAll()
        {
            return _productPrices.GetAll();
        }

        public ProductPrices GetById(int id)
        {
            return _productPrices.GetById(id);
        }

        public ProductPrices GetPriceByProductId(int id, bool IsWhat)
        {
            return _productPrices.GetPriceByProductId(id, IsWhat);  
        }

        public void Update(ProductPrices entity)
        {
            _productPrices.Update(entity);
        }
    }
}
