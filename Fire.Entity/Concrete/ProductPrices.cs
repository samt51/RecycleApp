using Fire.Entity.Abstrack;

namespace Fire.Entity.Concrete
{
    public class ProductPrices : IBaseEntity
    {
        public int Id { get; set; }
        public string Price { get; set; }
        public int ProductId { get; set; }
        /// <summary>
        /// 1 customer, 2 ise fabrika 3 ortak
        /// </summary>
        public bool IsWhat { get; set; }
    }
}
