using Fire.Entity.Abstrack;

namespace Fire.Entity.Concrete
{
    public class StockTracking:IBaseEntity
    {
        public int id { get; set; }
        public int productid { get; set; }
        public decimal totalkg { get; set; }
        public int branchid { get; set; }

    }
}
