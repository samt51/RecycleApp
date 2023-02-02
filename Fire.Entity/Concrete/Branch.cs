using Fire.Entity.Abstrack;

namespace Fire.Entity.Concrete
{
    public class Branch:IBaseEntity
    {
        public int id { get; set; }
        public string branch { get; set; }
        public int companyId { get; set; }
    }
}
