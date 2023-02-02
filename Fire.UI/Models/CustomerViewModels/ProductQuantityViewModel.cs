using Fire.Entity.Concrete;
using System.Collections.Generic;

namespace Fire.UI.Models.CustomerViewModels
{
    public class ProductQuantityViewModel
    {
        public int productId { get; set; }
        public string name{ get; set; }
        public decimal price { get; set; }
        public List<ProductQuantity> productQuantities{ get; set; }
        public decimal total { get; set; }
    }
    public class ProductquantityModel
    {
        public List<ProductQuantityViewModel> productQuantityViewModels { get; set; }
    }
}
