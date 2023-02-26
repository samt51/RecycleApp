using Fire.Business.Abstrack;
using Fire.Business.Encryption;
using Fire.Entity.Concrete;
using Fire.UI.Models;
using Fire.UI.Models.AllViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using static Fire.UI.Models.AuthorizationCont;

namespace Fire.UI.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeService _productTypeService;
        private readonly IProductQuantityService _productCategoriaService;
        private readonly IProductQuantityService _productQuantityService;
        private readonly IStockTrackingService _stockTrackingService;
        private readonly IPaymentContService _paymentContService;
        private readonly IReceiptService _receiptService;
        private readonly IProductPricesService _productPricesService;
        public ProductTypeController(IPaymentContService paymentContService, IProductPricesService productPricesService, IStockTrackingService stockTrackingService, IProductQuantityService productQuantityService
            , IProductQuantityService productCategoriaService, IProductTypeService productTypeService, IReceiptService receiptService)
        {
            _productTypeService = productTypeService;
            _productCategoriaService = productCategoriaService;
            _productQuantityService = productQuantityService;
            _stockTrackingService = stockTrackingService;
            _paymentContService = paymentContService;
            _receiptService = receiptService;
            _productPricesService = productPricesService;
        }
        [HttpGet]
        public IActionResult ProductTypeAdd()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
            });
        }
        [HttpPost]
        public IActionResult ProductTypeAdd(AllLayoutViewModel model)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var entity = new ProductType
            {
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                Name = model.ProductType.Name,
                İsDelete = false
            };
            var product = _productTypeService.Create(entity);
            var price = new ProductPrices
            {
                Price = "0",
                ProductId = product.id,
                IsWhat = true,
            };
            _productPricesService.Create(price);

            price = new ProductPrices
            {
                Price = "0",
                ProductId = product.id,
                IsWhat = false,
            };
            _productPricesService.Create(price);
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult List()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            return View(new AllLayoutViewModel
            {
                productTypes = _productTypeService.GetAll(),
                TokenKeys = keys,
            });
        }
        [HttpPost]
        public IActionResult FactoryProductType(string id)
        {
            var productid = CommondMethod.ConvertDecrypt(id);
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var productcategoria = _productTypeService.GetById(Convert.ToInt32(productid));
            var entity = new ProductType
            {
                IsWhat = 2,
                //kgPrice = productcategoria.kgPrice,
                Name = productcategoria.Name,
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                İsDelete = false,
                ProductQuantities = productcategoria.ProductQuantities,
                produckid = productcategoria.id,
            };
            _productTypeService.Create(entity);
            productcategoria.IsWhat = 3;
            _productTypeService.Update(productcategoria);
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Update(string id)
        {
            var productid = CommondMethod.ConvertDecrypt(id);
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var productcategoria = _productTypeService.GetById(Convert.ToInt32(productid));
            return View(new AllLayoutViewModel
            {
                ProductType = productcategoria,
                TokenKeys = keys,
            });
        }
        [HttpPost]
        public IActionResult Update(AllLayoutViewModel model)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var productid = HttpContext.Request.RouteValues["id"];
            productid = CommondMethod.ConvertDecrypt(productid.ToString());
            var product = _productTypeService.GetById(Convert.ToInt32(productid));
            product.Name = model.ProductType.Name;
            //product.kgPrice = model.ProductType.kgPrice;
            product.ModifyDate = System.DateTime.Now;
            _productTypeService.Update(product);
            return RedirectToAction("List");
        }
        [HttpGet]
        public List<ProductType> GetAll()
        {

            var ds = _productTypeService.GetAll();
            return ds;
        }
        [HttpPost]
        public List<string> deneme()
        {
            var products = _productTypeService.GetAll();
            var query = from d1 in products.Where(x => x.İsDelete == false)
                        join d2 in _productPricesService.GetAll() on d1.id equals d2.ProductId into gj
                        from subget in gj.DefaultIfEmpty()
                        where subget.İsDelete == false && subget.IsWhat == true
                        select new
                        {
                            Name = d1.Name,
                            Price = subget.Price == null ? "0" : subget.Price.ToString(),
                        };
            //obje türünde session doldurmak
            HttpContext.Session.SetString("sessionProductType", JsonConvert.SerializeObject(products));
            JsonConvert.SerializeObject(products);
            List<string> dd = new List<string>();
            foreach (var item in query.ToList())
            {
                dd.Add(item.Name + "," + item.Price);
            }
            return dd;
        }
        [HttpPost]
        public IActionResult ekle(AllLayoutViewModel model)
        {

            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var customerid = HttpContext.Session.GetString("customerid");
            HttpContext.Session.SetString("customerid", "");
            int id = Convert.ToInt32(customerid);



            if (model.Factory.CreatedDate.Year == 0001)
                model.Factory.CreatedDate = DateTime.Now;
            var quantitylist = _receiptService.GetReceiptByCustomerId(id, Convert.ToInt32(keys.branchid), true);
            var valuedate = quantitylist.Where(x => x.ReceiptDate == model.Factory.CreatedDate).FirstOrDefault();
            int quantity = 0;
            decimal totalkg = 0;
            decimal totalprice = 0;
            var data = new Receipt();
            if (valuedate != null)
            {
                var entity = new Receipt
                {
                    BranchId = Convert.ToInt32(keys.branchid),
                    DailyTakeCount = valuedate.DailyTakeCount + 1,
                    CustomerId = id,
                    ReceiptDate = model.Factory.CreatedDate,
                    İsDelete = false,
                    CreatedDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    IsWhat = true,
                };
                data = _receiptService.Create(entity);
            }
            else
            {
                var entity = new Receipt
                {
                    BranchId = Convert.ToInt32(keys.branchid),
                    DailyTakeCount = 1,
                    CustomerId = id,
                    ReceiptDate = model.Factory.CreatedDate,
                    İsDelete = false,
                    CreatedDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    IsWhat = true,
                };
                data = _receiptService.Create(entity);
            }


            var gelenform = HttpContext.Request.Form.ToList().Where(x => x.Value != "0" && x.Key != "__RequestVerificationToken" && x.Key != "Factory.CreatedDate" && x.Value.Count > 1).ToList();
            for (int i = 0; i < gelenform.Count; i++)
            {
                string key = gelenform[i].Key.ToString();
                var value = gelenform[i].Value.ToList();
                string[] arra = key.Split(',');
                var keyname = arra[0];
                var keyvalue = arra[1];
                var s = keyvalue.ToString().IndexOf("_");
                keyvalue = keyvalue.Remove(s);
                var pric = 0.0;

                var productypeid = _productTypeService.GetProductTypeByName(keyname);
                var price = _productPricesService.GetPriceByProductId(productypeid.id, true);
                for (int j = 1; j < value.Count; j++)
                {
                    if (string.IsNullOrEmpty(value[j]))
                        break;
                    if (Convert.ToDecimal(value[j].Replace(",", ".")) > 0)
                    {
                        if (Convert.ToDecimal(value[0]) == 0)
                            pric = Convert.ToDouble(price.Price);
                        else
                            pric = Convert.ToDouble(value[0].Replace(",", "."));
                        totalkg += Convert.ToDecimal(value[j]);
                        totalprice += Convert.ToDecimal(value[j]) * Convert.ToDecimal(pric);
                        var entity = new ProductQuantity
                        {
                            Name = keyname,
                            Totalkg = 0,
                            Totalprice = Convert.ToDecimal(value[j]) * Convert.ToDecimal(pric),
                            UnitPrice = Convert.ToDecimal(pric),
                            ReceiptId = data.id,
                            productTypeid = productypeid.id,
                            Kg = Convert.ToDecimal(value[j]),
                            İsDelete = false,
                            CreatedDate = model.Factory.CreatedDate,
                            ModifyDate = DateTime.Now,
                            ProductPrice = Convert.ToDecimal(price.Price),


                        };
                        _productQuantityService.Create(entity);
                        var stockcontroll = _stockTrackingService.GetStockByProductId(productypeid.id, Convert.ToInt32(keys.branchid));
                        if (stockcontroll != null)
                        {
                            stockcontroll.totalkg += entity.Kg;
                            stockcontroll.ModifyDate = DateTime.Now;
                            _stockTrackingService.Update(stockcontroll);
                        }
                        else
                        {
                            var stockentity = new StockTracking
                            {
                                CreatedDate = new DateTime(model.Factory.CreatedDate.Year, model.Factory.CreatedDate.Month, model.Factory.CreatedDate.Day),
                                ModifyDate = new DateTime(model.Factory.CreatedDate.Year, model.Factory.CreatedDate.Month, model.Factory.CreatedDate.Day),
                                productid = productypeid.id,
                                totalkg = entity.Kg,
                                İsDelete = false,
                                branchid = Convert.ToInt32(keys.branchid),
                            };
                            _stockTrackingService.Create(stockentity);
                        }
                    }
                    else
                        break;
                }


            }

            return RedirectToAction("List", "Customer");
        }
        [HttpPost]
        public IActionResult ReceiptUpdate(AllLayoutViewModel model)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var customerid = HttpContext.Session.GetString("customerid");
            HttpContext.Session.SetString("customerid", "");
            int id = Convert.ToInt32(customerid);
            var updateJson = HttpContext.Session.GetString("updateJson");
            if (!string.IsNullOrEmpty(updateJson) && updateJson != "99")
            {
                HttpContext.Session.SetString("updateJson", "99");

                var update = JsonConvert.DeserializeObject<ProductTypeUpdateDataModel>(updateJson);
                var gelenform = HttpContext.Request.Form.ToList().Where(x => x.Value != "0" && x.Key != "__RequestVerificationToken" && x.Key != "Factory.CreatedDate" && x.Value.Count > 1).ToList();
                var list = _receiptService.GetReceiptByCustomerId(update.customerid, Convert.ToInt32(keys.branchid), true);
                var receiptData = list.Where(x => x.ReceiptDate.Year == update.date.Year && x.ReceiptDate.Month == update.date.Month && x.ReceiptDate.Day == update.date.Day && x.DailyTakeCount == update.quantity).FirstOrDefault();
                var productquantity = new List<ProductQuantity>();

                if (receiptData != null)
                {
                    productquantity = _productQuantityService.GetQuantityByReceiptBy(receiptData.id);

                }
                var price = decimal.Zero;
                decimal totalkg = 0;
                decimal totalprice = 0;
                for (int i = 0; i < gelenform.Count; i++)
                {
                    string[] splitKey = gelenform[i].Key.Split(",");
                    var value = gelenform[i].Value.ToList();
                    var data = productquantity.Where(x => x.Name == splitKey[0]).ToList();
                    var firstData = productquantity.Where(x => x.Name == splitKey[0]).FirstOrDefault();
                    var valueData = value.ToList();
                    var productType = _productTypeService.GetProductTypeByName(splitKey[0].ToString());
                    var productPrice = _productPricesService.GetPriceByProductId(productType.id, true);
                    if (Convert.ToDecimal(value[0]) == 0 || string.IsNullOrEmpty(value[0]))
                    {
                        price = Convert.ToDecimal(productPrice.Price);
                    }
                    else
                    {
                        price = Convert.ToDecimal(value[0].Replace(",", "."));
                    }
                    value.RemoveAt(0);
                    for (int k = 0; k < value.Count; k++)
                    {
                        if (firstData == null)
                        {
                            if (value[k] == "")
                            {
                                break;

                            }
                            if (Convert.ToDecimal(value[k]) == 0 || string.IsNullOrEmpty(value[k]))
                            {
                                break;
                            }
                            totalkg += Convert.ToDecimal(value[k]);
                            totalprice += Convert.ToDecimal(value[k]) * Convert.ToDecimal(price);
                            var entity = new ProductQuantity
                            {
                                Name = splitKey[0],
                                Totalkg = 0,
                                Totalprice = Convert.ToDecimal(value[k]) * price,
                                ReceiptId = receiptData.id,
                                UnitPrice = price,
                                ProductPrice = Convert.ToDecimal(productPrice.Price),
                                productTypeid = productType.id,
                                Kg = Convert.ToDecimal(value[k]),
                                İsDelete = false,
                                CreatedDate = update.date,
                                ModifyDate = DateTime.Today,

                            };
                            var stockcontroll = _stockTrackingService.GetStockByProductId(productType.id, Convert.ToInt32(keys.branchid));
                            if (stockcontroll == null)
                            {
                                var stockentity = new StockTracking
                                {
                                    CreatedDate = DateTime.Now,
                                    ModifyDate = DateTime.Now,
                                    productid = productType.id,
                                    totalkg = entity.Kg,
                                    İsDelete = false,
                                    branchid = Convert.ToInt32(keys.branchid),
                                };
                                _productQuantityService.Create(entity);
                                _stockTrackingService.Create(stockentity);
                            }
                            else
                            {
                                _productQuantityService.Create(entity);

                                stockcontroll.totalkg += entity.Kg;
                                stockcontroll.ModifyDate = DateTime.Now;
                                _stockTrackingService.Update(stockcontroll);
                            }


                        }
                        else
                        {
                            if (k >= data.Count)
                            {
                                if (Convert.ToDecimal(value[k]) == 0 || string.IsNullOrEmpty(value[k]))
                                {
                                    break;
                                }
                                else
                                {
                                    totalkg += Convert.ToDecimal(value[k]);
                                    totalprice += Convert.ToDecimal(value[k]) * Convert.ToDecimal(price);
                                    var entity = new ProductQuantity
                                    {
                                        Name = splitKey[0],
                                        Totalkg = 0,
                                        Totalprice = Convert.ToDecimal(value[k]) * Convert.ToDecimal(price),
                                        ProductPrice = Convert.ToDecimal(productPrice.Price),
                                        UnitPrice = Convert.ToDecimal(price),
                                        ReceiptId = receiptData.id,
                                        productTypeid = productType.id,
                                        Kg = Convert.ToDecimal(value[k]),
                                        İsDelete = false,
                                        CreatedDate = update.date,
                                        ModifyDate = DateTime.Today,

                                    };
                                    _productQuantityService.Create(entity);
                                    var stockcontroll = _stockTrackingService.GetStockByProductId(productType.id, Convert.ToInt32(keys.branchid));
                                    stockcontroll.totalkg += entity.Kg;
                                    stockcontroll.ModifyDate = DateTime.Now;
                                    _stockTrackingService.Update(stockcontroll);

                                }
                            }

                            else
                            {
                                if (k == data.Count)
                                {

                                }
                                else
                                {
                                    if (Convert.ToDecimal(value[k]) != data[k].Kg)
                                    {
                                        totalkg += Convert.ToDecimal(value[k]);
                                        totalprice += Convert.ToDecimal(value[k]) * Convert.ToDecimal(price);
                                        decimal eksi = decimal.Zero;
                                        if (data[k].Kg > Convert.ToDecimal(value[k]))//stoktan çıkaracak
                                        {
                                            eksi = data[k].Kg - Convert.ToDecimal(value[k]);
                                            data[k].Kg = Convert.ToDecimal(value[k]);
                                            data[k].ModifyDate = DateTime.Now;
                                            data[k].Totalprice = data[k].Kg * price;
                                            _productQuantityService.Update(data[k]);
                                            var stockcontroll = _stockTrackingService.GetStockByProductId(productType.id, Convert.ToInt32(keys.branchid));
                                            stockcontroll.totalkg = stockcontroll.totalkg - eksi;
                                            stockcontroll.ModifyDate = DateTime.Now;
                                            _stockTrackingService.Update(stockcontroll);

                                        }
                                        else
                                        {
                                            eksi = Convert.ToDecimal(value[k]) - data[k].Kg;
                                            data[k].Kg = Convert.ToDecimal(value[k]);
                                            data[k].ModifyDate = DateTime.Now;
                                            data[k].Totalprice = data[k].Kg * price;
                                            _productQuantityService.Update(data[k]);
                                            var stockcontroll = _stockTrackingService.GetStockByProductId(productType.id, Convert.ToInt32(keys.branchid));
                                            stockcontroll.totalkg = stockcontroll.totalkg + eksi;
                                            stockcontroll.ModifyDate = DateTime.Now;
                                            _stockTrackingService.Update(stockcontroll);
                                        }



                                    }
                                }


                            }
                        }
                        //eklenmiş ürün cinclerini kg update işlemleri
                    }
                }
                return RedirectToAction("List", "Customer");

            }
            return RedirectToAction("List", "Customer");
        }
        [HttpGet]
        public IActionResult Add(string id)
        {
            var productid = CommondMethod.ConvertDecrypt(id);
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var product = _productTypeService.GetById(Convert.ToInt32(productid));
            HttpContext.Session.SetString("customerid", productid.ToString());
            return View(new AllLayoutViewModel
            {
                ProductType = product,
                TokenKeys = keys,
            });
        }
        [HttpGet]
        public IActionResult AddProductWithCustomerid(string producttypeid)
        {
            var productid = CommondMethod.ConvertToEncrypt(producttypeid);
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            return View(new AllLayoutViewModel
            {
                ProductType = _productTypeService.GetById(Convert.ToInt32(productid)),
                TokenKeys = keys,
            });
        }
        //[HttpPost]
        //public IActionResult AddProductWithCustomerid(ValueSum values)
        //{
        //    var Authorization = HttpContext.Session.GetString("token");
        //    TokenKeys keys = AuthorizationCont.Authorization(Authorization);
        //    if (keys == null)
        //        return Redirect("/Error/401");
        //    var producttypeid = HttpContext.Request.RouteValues["id"];
        //    var customerid = HttpContext.Session.GetString("customerid");
        //    decimal sumtotal = 0;
        //    var entity = new ProductQuantity();
        //    List<ProductQuantity> productQuantities = new List<ProductQuantity>();
        //    foreach (var item in values.value)
        //    {
        //        //entity = new ProductQuantity();
        //        //entity.CreatedDate = System.DateTime.Now;
        //        //entity.Customerid = Convert.ToInt32(customerid);
        //        //entity.TypeProductid = Convert.ToInt32(producttypeid);
        //        //entity.İsDelete = false;
        //        //entity.Quantity = item;
        //        //entity.ModifyDate = DateTime.Now;
        //        sumtotal += item;
        //        _productQuantityService.Create(entity);
        //    }
        //    var value = _productTypeService.GetById(2);
        //    var categoria = new ProductQuantity
        //    {
        //        Name = value.Name,
        //        CreatedDate = DateTime.Now,
        //        customerid = entity.customerid,
        //        ModifyDate = DateTime.Now,
        //        Totalkg = sumtotal,
        //        Totalprice = sumtotal * value.kgPrice,
        //        UnitPrice = value.kgPrice,
        //        İsDelete = false
        //    };
        //    _productCategoriaService.Create(categoria);

        //    return Redirect("/ProductType/Add/" + customerid);
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var data = _productTypeService.GetById(id);
            data.İsDelete = true;
            _productTypeService.Update(data);
            return RedirectToAction("List", "ProductType");
        }
    }
}
