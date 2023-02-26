using Fire.DataAccess.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using System.Linq;

namespace Fire.DataAccess.Concrete.Ef
{
    public class EfProductPricesDal : EfRepositoryDal<ProductPrices, DataContext>, IProductPricesDal
    {
        public ProductPrices GetPriceByProductId(int id, bool IsWhat)
        {
            using (var db = new DataContext())
            {
                var data = db.productPrices.Where(x => x.IsWhat == IsWhat && x.ProductId == id && x.İsDelete == false).FirstOrDefault();
                if (data == null)
                    return null;
                return data;
            }
        }
    }
}
