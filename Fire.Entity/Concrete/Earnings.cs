using Fire.Entity.Abstrack;

namespace Fire.Entity.Concrete
{
    public class Earnings:IBaseEntity
    {
        public int id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
