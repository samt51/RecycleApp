using System;

namespace Fire.Entity.Abstrack
{
    public abstract class IBaseEntity
    {
        public bool İsDelete { get; set; }
        public DateTime CreatedDate{ get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
