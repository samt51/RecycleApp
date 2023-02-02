using Fire.Entity.Abstrack;
using System.Collections.Generic;

namespace Fire.Entity.Concrete
{
    public class UserRoles:IBaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<User> Users{ get; set; }
    }
}
