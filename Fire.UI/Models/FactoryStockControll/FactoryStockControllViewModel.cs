using Fire.Business.Abstrack;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fire.UI.Models.FactoryStockControll
{
    public class FactoryStockControllViewModel
    {
        public static object Controll(int quantity, List<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>> filter, IProductTypeService productTypeService, IStockTrackingService stockTrackingService)
        {

            var factorystock = FactoryStockControllViewModel.stockcontroll(filter, stockTrackingService, productTypeService,3);
            if (!string.IsNullOrEmpty(factorystock))
                return factorystock;

            for (int i = 0; i < filter.Count; i++)
            {
                string key = filter[i].Key.ToString();
                var value = filter[i].Value.ToList();
                string[] arra = key.Split(',');
                var keyname = arra[0];
                var keyvalue = arra[1];
                var val = keyvalue.ToString().IndexOf("_");
                keyvalue = keyvalue.Remove(val);
                var productid = productTypeService.GetProductTypeByName(keyname);
                for (int j = 1; j < value.Count; j++)
                {
                    if (string.IsNullOrEmpty(value[j]))
                        break;
                    if (Convert.ToDecimal(value[j].Replace(",", ".")) > 0)
                    {
                        var entity = new FactoryQuantity
                        {
                            Name = keyname,
                            Totalkg = 0,
                            Totalprice = Convert.ToDecimal(value[j]) * Convert.ToDecimal(value[0]),
                            
                            UnitPrice = Convert.ToDecimal(value[0]),
 
                            factoryproducttypeid = productid.id,
                            İsDelete = false,
                            CreatedDate = DateTime.Today,
                            ModifyDate = DateTime.Today,
                            Kg = Convert.ToDecimal(value[j]),
                        };


                    }
                    else
                        break;
                }
            }
            return "";
        }
        public static string stockcontroll(List<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>> value, IStockTrackingService stockTrackingService, IProductTypeService productTypeService,int keys)
        {
            string keyname = null;
            var errors=new List<string>();
            string errorName = null;
            for (int i = 0; i < value.Count; i++)
            {
                string key = value[i].Key.ToString();
                var deger = value[i].Value.ToList();
                string[] arra = key.Split(',');
                keyname = arra[0];
                var keyvalue = arra[1];
                var s = keyvalue.ToString().IndexOf("_");
                keyvalue = keyvalue.Remove(s);
                decimal toplam = 0;
                var producttypeid = productTypeService.GetProductTypeByName(keyname);
                var stock = stockTrackingService.GetStockByProductId(producttypeid.id,keys);
                if (stock == null)
                {
                    keyname = $"{keyname} ürünün stok bulunmamaktadır. Kontrol ediniz!";
                    return keyname;
                }
                else if (deger[1] == "0")
                {
                    keyname = null;
                    return keyname;
                }
                else
                {
                    for (int k = 1; k < deger.Count; k++)
                    {
                        if (Convert.ToDecimal(deger[k].Replace(",", ".")) == 0)
                            break;
                        toplam += Convert.ToDecimal(deger[k]);
                    }
                    if (toplam > stock.totalkg)
                    {

                        keyname=$"{keyname} ürünün stok kg yeterli bulunmamaktadır. Lütfen güncelleyin";
                        return keyname;
                    }
                    else
                    {
                        keyname = null;
                        return key;
                    }
                  
                }
            }
            return $"{keyname}";
        }
        public int productId { get; set; }
        public decimal price { get; set; }
        public decimal total { get; set; }
        public string name { get; set; }
        public decimal productPrice { get; set; }
        public List<FactoryQuantity> factoryQuantities { get; set; }
    }
}
