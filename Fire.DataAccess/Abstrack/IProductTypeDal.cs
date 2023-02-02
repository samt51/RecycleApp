using Fire.Entity.Concrete;

namespace Fire.DataAccess.Abstrack
{
    public interface IProductTypeDal:IRepository<ProductType>
    {
        ProductType GetProductTypeByName(string name);
    }
}
