using Fire.Entity.Abstrack;

namespace Fire.Entity.Concrete
{
    public class ProductQuantity : IBaseEntity
    {
        public int id { get; set; }
        public string Name { get; set; }
        public decimal Totalkg { get; set; }
        public decimal Totalprice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ProductPrice { get; set; }
        public int ReceiptId { get; set; }
        public int productTypeid { get; set; }
        public ProductType productType { get; set; }
        public decimal Kg { get; set; }
        public bool paid { get; set; }
        public decimal alacak { get; set; }
        
    }
}
