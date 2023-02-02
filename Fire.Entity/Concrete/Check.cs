using Fire.Entity.Abstrack;
using System;

namespace Fire.Entity.Concrete
{
    public class Check:IBaseEntity
    {
        public int id { get; set; }
        public string checkNumber { get; set; }
        public int bankid { get; set; }
        public decimal price { get; set; }
        public DateTime checkDate{ get; set; }
        public int whoFromGetted { get; set; }
        public int? toWhoWasGiven { get; set; }
        public int branchid { get; set; }
        /// <summary>
        /// true customer, false ise fabrika
        /// </summary>
        public bool IsWhat { get; set; }

    }
}
