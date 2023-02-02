using Fire.Entity.Abstrack;

namespace Fire.Entity.Concrete
{
    public class Company:IBaseEntity
    {
        public int id { get; set; }
        public string Name {get; set; }
    }
}
