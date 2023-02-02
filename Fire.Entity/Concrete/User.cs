using Fire.Entity.Abstrack;
using System.ComponentModel.DataAnnotations;

namespace Fire.Entity.Concrete
{
    public class User:IBaseEntity
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int userroleid { get; set; }
        public UserRoles userRoles { get; set; }
        public int? branchid { get; set; }
        public int companyId { get; set; }
    }
}
