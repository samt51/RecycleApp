using Fire.Entity.Abstrack;
using System;

namespace Fire.Entity.Concrete
{
    public class Receipt:IBaseEntity
    {
        public int id { get; set; }
        public DateTime ReceiptDate{ get; set; }
        public int DailyTakeCount { get; set; }
        public int BranchId { get; set; }
        public int CustomerId { get; set; }
        /// <summary>
        /// true customer, false ise fabrika
        /// </summary>
        public bool IsWhat { get; set; }
    }
}
