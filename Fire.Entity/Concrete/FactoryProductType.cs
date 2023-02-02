using System.Collections.Generic;

namespace Fire.Entity.Concrete
{
    public class FactoryProductType
    {
        public int id { get; set; }
        public string Name { get; set; }
        public decimal kgPrice { get; set; }
        public int factoryid { get; set; }
        public Factory factory { get; set; }
    }
}
