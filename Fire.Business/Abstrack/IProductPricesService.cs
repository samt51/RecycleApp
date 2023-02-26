using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.Business.Abstrack
{
    public interface IProductPricesService
    {
        ProductPrices GetPriceByProductId(int id, bool IsWhat);
        List<ProductPrices> GetAll();
        ProductPrices GetById(int id);
        void Create(ProductPrices entity);
        void Delete(ProductPrices entity);
        void Update(ProductPrices entity);
    }
}
