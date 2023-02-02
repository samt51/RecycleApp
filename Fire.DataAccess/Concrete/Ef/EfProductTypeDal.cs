using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using System.Linq;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfProductTypeDal : EfRepositoryDal<ProductType, DataContext>, IProductTypeDal
    {
        public ProductType GetProductTypeByName(string name)
        {
            using (var db=new DataContext())
            {
                return db.ProductTypes.Where(x => x.Name == name).FirstOrDefault();
            }
        }
    }
}
