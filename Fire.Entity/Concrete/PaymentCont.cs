using Fire.Entity.Abstrack;

namespace Fire.Entity.Concrete
{
    public class PaymentCont : IBaseEntity
    {
        public int id { get; set; }
        public int ReceiptId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        /// <summary>
        /// true customer, false ise fabrika
        /// </summary>
        public bool IsWhat { get; set; }
    }
}
