using Fire.Entity.Abstrack;
using System.ComponentModel.DataAnnotations;

namespace Fire.Entity.Concrete
{
    public class Employees:IBaseEntity
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal Salary { get; set; }
        public int branchid { get; set; }

    }
}
