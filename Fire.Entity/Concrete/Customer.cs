using Fire.Entity.Abstrack;
using System.ComponentModel.DataAnnotations;

namespace Fire.Entity.Concrete
{
    public class Customer:IBaseEntity
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public int companyId { get; set; }

    }
}
