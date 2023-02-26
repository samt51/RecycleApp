using Fire.Entity.Concrete;

namespace Fire.DataAccess.Abstrack
{
    public interface IProductPricesDal : IRepository<ProductPrices>
    {
        ProductPrices GetPriceByProductId(int id,bool IsWhat);
    }
}
