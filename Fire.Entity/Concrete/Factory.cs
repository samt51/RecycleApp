using Fire.Entity.Abstrack;
using System.Collections.Generic;

namespace Fire.Entity.Concrete
{
    public class Factory:IBaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Phone { get; set; }
        public int companyId { get; set; }
        public List<FactoryProductType> factoryProductTypes { get; set; }
    }
}
