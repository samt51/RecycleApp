using Fire.Entity.Abstrack;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fire.Entity.Concrete
{
    public class ProductType:IBaseEntity
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 1 customer, 2 ise fabrika 3 ortak
        /// </summary>
        public int IsWhat { get; set; }
        public int? produckid { get; set; }
        public List<ProductQuantity> ProductQuantities { get; set; }

    }
}
