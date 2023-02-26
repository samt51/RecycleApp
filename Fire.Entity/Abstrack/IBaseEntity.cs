using System;

namespace Fire.Entity.Abstrack
{
    public abstract class IBaseEntity
    {
        public bool İsDelete { get; set; }=false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
    }
}
