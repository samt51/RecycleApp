using Fire.Business.Abstrack;
using Fire.Business.Encryption;
using Fire.Entity.Concrete;
using Fire.UI.Models;
using Fire.UI.Models.AllViewModels;
using Fire.UI.Models.FactoryStockControll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using static Fire.UI.Models.AuthorizationCont;

namespace Fire.UI.Controllers
{
    public class FactoryController : Controller
    {
        private readonly IFactoryService _factoryservice;
        private readonly IFactoryQuantityService _factoryQuantityService;
        private readonly IProductTypeService _productTypeService;
        private readonly IStockTrackingService _stockTrackingService;
        private readonly IPaymentContService _paymentContService;
        private readonly IReceiptService _receiptService;
        private readonly IProductPricesService _productPricesService;
        public FactoryController(IProductPricesService productPricesService, IPaymentContService paymentContService, IStockTrackingService stockTrackingService, IFactoryQuantityService factoryQuantityService,
            IProductTypeService productTypeService, IFactoryService factoryservice, IReceiptService receiptService)
        {
            _productPricesService = productPricesService;
            _factoryservice = factoryservice;
            _factoryQuantityService = factoryQuantityService;
            _productTypeService = productTypeService;
            _stockTrackingService = stockTrackingService;
            _paymentContService = paymentContService;
            _receiptService = receiptService;
        }
        #region
        public IActionResult List()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            ViewBag.results = _factoryservice.GetAll();
            return View(new AllLayoutViewModel
            {
                Factories = _factoryservice.GetAll(),
                TokenKeys = keys,
            });
        }
        public IActionResult AddFactory()
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
        public IActionResult AddFactory(AllLayoutViewModel model)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var factory = new Factory
            {
                CreatedDate = System.DateTime.Now,
                ModifyDate = System.DateTime.Now,
                İsDelete = false,
                name = model.Factory.name,
                Phone = model.Factory.Phone,
                companyId = Convert.ToInt32(keys.companyId)
            };
            _factoryservice.Create(factory);
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Update(string id)
        {
            var factoryid = CommondMethod.ConvertDecrypt(id);
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var value = _factoryservice.GetById(Convert.ToInt32(factoryid));
            HttpContext.Session.SetString("FactoryId", id);
            return View(new AllLayoutViewModel
            {
                Factory = value,
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
            var factoryid = HttpContext.Session.GetString("FactoryId");
            var deger = _factoryservice.GetById(Convert.ToInt32(CommondMethod.ConvertDecrypt(factoryid.ToString())));
            deger.name = model.Factory.name;
            deger.Phone = model.Factory.Phone;
            deger.ModifyDate = System.DateTime.Now;
            _factoryservice.Update(deger);
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var customer = _factoryservice.GetById(id);
            customer.İsDelete = true;
            _factoryservice.Update(customer);
            return RedirectToAction("List");
        }
        #endregion
        [HttpGet]
        public IActionResult FactoryReceptAddByFactoryid(string id)
        {
            var ids = CommondMethod.ConvertDecrypt(id);
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            HttpContext.Session.SetString("factoryId", ids);
            var message = HttpContext.Session.GetString("factoryError");
            if (!string.IsNullOrEmpty(message) && message != "00")
            {
                ViewBag.message = message;
                HttpContext.Session.SetString("factoryError", "00");
            }
            HttpContext.Session.SetString("updatefactory", "99");

            return View(new AllLayoutViewModel
            {
                productTypes = _productTypeService.GetAll(),
                TokenKeys = keys,
            });
        }
        [HttpPost]
        public IActionResult FactoryReceptAddByFactoryid(AllLayoutViewModel allLayoutViewModel)
        {

            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var factoryid = HttpContext.Session.GetString("factoryId");
            int id = Convert.ToInt32(factoryid);

            if (allLayoutViewModel.Factory.CreatedDate.Year == 0001)
                allLayoutViewModel.Factory.CreatedDate = DateTime.Now;
            var quantitylist = _receiptService.GetReceiptByCustomerId(id, Convert.ToInt32(keys.branchid), false);

            var valuedate = quantitylist.Where(x => x.ReceiptDate.Year == allLayoutViewModel.Factory.CreatedDate.Year && x.ReceiptDate.Month == allLayoutViewModel.Factory.CreatedDate.Month && x.ReceiptDate.Day == allLayoutViewModel.Factory.CreatedDate.Day).FirstOrDefault();
            int quantity = 0;
            decimal totalkg = 0;
            decimal totalprice = 0;
            var data = new Receipt();
            var receiptEntity = new Receipt();
            if (valuedate != null)
            {
                receiptEntity = new Receipt
                {
                    BranchId = Convert.ToInt32(keys.branchid),
                    DailyTakeCount = valuedate.DailyTakeCount + 1,
                    CustomerId = id,
                    ReceiptDate = allLayoutViewModel.Factory.CreatedDate,
                    İsDelete = false,
                    CreatedDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    IsWhat = false,
                };

            }
            else
            {
                receiptEntity = new Receipt
                {
                    BranchId = Convert.ToInt32(keys.branchid),
                    DailyTakeCount = 1,
                    CustomerId = id,
                    ReceiptDate = allLayoutViewModel.Factory.CreatedDate,
                    İsDelete = false,
                    CreatedDate = DateTime.Now,
                    ModifyDate = DateTime.Now
                };

            }
            data = _receiptService.Create(receiptEntity);
            var gelenform = HttpContext.Request.Form.ToList().Where(x => x.Value != "0" && x.Value.Count > 1 && x.Key != "__RequestVerificationToken" && x.Key != "Factory.CreatedDate").ToList();
            //var factorystockvalue = FactoryStockControllViewModel.stockcontroll(gelenform, _stockTrackingService, _productTypeService, Convert.ToInt32(keys.branchid));
            for (int i = 0; i < gelenform.Count; i++)
            {
                string key = gelenform[i].Key.ToString();

                var value = gelenform[i].Value.ToList();
                string[] arra = key.Split(',');
                var keyname = arra[0];
                var keyvalue = arra[1];
                var s = keyvalue.ToString().IndexOf("_");
                keyvalue = keyvalue.Remove(s);
                var productypeid = _productTypeService.GetProductTypeByName(keyname);
                var productPrice = _productPricesService.GetPriceByProductId(productypeid.id, false);
                decimal val = 0;
                decimal tpl = 0;
                int tok = 0;

                if (value.Count > 0)
                {
                    var stockcontroll = new StockTracking();

                    if (Convert.ToDecimal(value[1]) > 0)
                    {
                        stockcontroll = _stockTrackingService.GetStockByProductId(productypeid.id, Convert.ToInt32(keys.branchid));
                        if (stockcontroll is null)
                        {
                            ViewBag.message = $"{productypeid.Name} ürünün stokta mevcut değildir.Kontrol ediniz";
                            return View(new AllLayoutViewModel { TokenKeys = keys }); ;
                        }
                    }






                    for (int k = 1; k < value.Count; k++)
                    {
                        if (Convert.ToDecimal(value[k]) > 0)
                        {
                            tpl += Convert.ToDecimal(value[k]);
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (tpl > stockcontroll.totalkg)
                    {
                        ViewBag.message = $"{productypeid.Name} ürünün yeterince bulunmamaktadır.Güncelleme Yapınız.";
                        return View(new AllLayoutViewModel { TokenKeys = keys }); ;
                    }
                    else
                    {
                        for (int j = 1; j < value.Count; j++)
                        {
                            if (Convert.ToDecimal(value[1]) == 0)
                            {
                                break;
                            }

                            if (string.IsNullOrEmpty(value[j]))
                                break;
                            if (Convert.ToDecimal(value[j].Replace(",", ".")) > 0)
                            {
                                if (Convert.ToDecimal(value[0]) == 0)
                                    val = Convert.ToDecimal(productPrice.Price);
                                else
                                    val = Convert.ToDecimal(value[0].Replace(",", "."));

                                stockcontroll.totalkg = stockcontroll.totalkg - Convert.ToDecimal(value[j]);
                                stockcontroll.ModifyDate = DateTime.Now;

                                var entity = new FactoryQuantity
                                {
                                    Name = keyname,
                                    Totalkg = 0,
                                    Totalprice = Convert.ToDecimal(value[j]) * val,
                                    UnitPrice = val,
                                    ReceiptId = data.id,
                                    factoryproducttypeid = productypeid.id,
                                    İsDelete = false,
                                    CreatedDate = allLayoutViewModel.Factory.CreatedDate,
                                    ModifyDate = DateTime.Now,
                                    Kg = Convert.ToDecimal(value[j]),
                                    ProductPrice = Convert.ToDecimal(productPrice.Price),
                                };
                                _factoryQuantityService.Create(entity);
                                _stockTrackingService.Update(stockcontroll);


                            }
                            else
                            {
                                break;
                            }

                        }
                    }

                }

            }

            return Redirect("/Factory/GetBeforeReceiptByFactoryid/" + CommondMethod.ConvertToEncrypt(id.ToString()));

        }
        [HttpPost]
        public IActionResult FactoryReceiptUpdate(AllLayoutViewModel allLayoutViewModel)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var factoryid = HttpContext.Session.GetString("factoryId");
            int id = Convert.ToInt32(factoryid);
            var update = HttpContext.Session.GetString("updatefactory");
            if (!string.IsNullOrEmpty(update) && update != "99")
            {
                var updated = JsonConvert.DeserializeObject<ProductTypeUpdateDataModel>(update);
                var gelenform = HttpContext.Request.Form.ToList().Where(x => x.Value != "0" && x.Value.Count > 1 && x.Key != "__RequestVerificationToken" && x.Key != "Factory.CreatedDate" && x.Value.Count > 1).ToList();
                var list = _receiptService.GetReceiptByCustomerId(updated.customerid, Convert.ToInt32(keys.branchid), false);
                var receiptData = list.Where(x => x.ReceiptDate.Year == updated.date.Year && x.ReceiptDate.Month == updated.date.Month && x.ReceiptDate.Day == updated.date.Day && x.DailyTakeCount == updated.quantity).FirstOrDefault();

                var productquantity = new List<FactoryQuantity>();


                if (receiptData != null)
                {
                    productquantity = _factoryQuantityService.GetFactoryQuantitiesByFactoryid(receiptData.id);

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
                    var productPrice = _productPricesService.GetPriceByProductId(productType.id, false);
                    if (Convert.ToDecimal(value[0]) == 0 || string.IsNullOrEmpty(value[0]))
                    {
                        price = Convert.ToDecimal(productPrice.Price);
                    }
                    else
                    {
                        price = Convert.ToDecimal(value[0].Replace(",", "."));
                    }


                    value.RemoveAt(0);


                    var productTotalkg = data.Sum(x => x.Kg);
                    decimal tpl = 0;


                    foreach (var item in value)
                    {
                        if (Convert.ToDecimal(item) > 0)
                        {
                            tpl += Convert.ToDecimal(item);

                        }
                        else
                        {
                            break;
                        }
                    }
                    var sumKg = Math.Abs(tpl - productTotalkg);

                    var stockcontroll = _stockTrackingService.GetStockByProductId(productType.id, Convert.ToInt32(keys.branchid));

                    if (stockcontroll != null && stockcontroll.totalkg >= sumKg && sumKg != 0 && stockcontroll.totalkg != 0)
                    {

                        for (int j = 0; j < value.Count; j++)
                        {
                            if (Convert.ToDecimal(value[0]) == 0 && tpl == 0)
                            {
                                break;
                            }
                            if (firstData == null)
                            {
                                if (Convert.ToDecimal(value[j]) == 0 || string.IsNullOrEmpty(value[j]))
                                {
                                    break;
                                }
                                totalkg += Convert.ToDecimal(value[j]);
                                totalprice += Convert.ToDecimal(value[j]) * Convert.ToDecimal(price);
                                var entity = new FactoryQuantity
                                {
                                    Name = splitKey[0],
                                    Totalkg = 0,
                                    Totalprice = Convert.ToDecimal(value[j]) * price,
                                    UnitPrice = price,
                                    ReceiptId = receiptData.id,
                                    factoryproducttypeid = productType.id,
                                    İsDelete = false,
                                    CreatedDate = updated.date,
                                    ModifyDate = DateTime.Now,
                                    Kg = Convert.ToDecimal(value[j]),
                                    ProductPrice = Convert.ToDecimal(productPrice.Price)
                                };

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
                                    _factoryQuantityService.Create(entity);
                                    _stockTrackingService.Create(stockentity);
                                }
                                else
                                {
                                    _factoryQuantityService.Create(entity);

                                    stockcontroll.totalkg -= entity.Kg;
                                    stockcontroll.ModifyDate = DateTime.Now;
                                    _stockTrackingService.Update(stockcontroll);
                                }



                            }
                            else
                            {
                                if (j >= data.Count)
                                {
                                    if (Convert.ToDecimal(value[j]) == 0 || string.IsNullOrEmpty(value[j]))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        totalkg += Convert.ToDecimal(value[j]);
                                        totalprice += Convert.ToDecimal(value[j]) * price;
                                        var entity = new FactoryQuantity
                                        {
                                            Name = splitKey[0],
                                            Totalkg = 0,
                                            Totalprice = Convert.ToDecimal(value[j]) * price,
                                            ProductPrice = Convert.ToDecimal(productPrice.Price),
                                            UnitPrice = Convert.ToDecimal(price),
                                            ReceiptId = receiptData.id,
                                            factoryproducttypeid = productType.id,
                                            Kg = Convert.ToDecimal(value[j]),
                                            İsDelete = false,
                                            CreatedDate = updated.date,
                                            ModifyDate = DateTime.Today

                                        };
                                        _factoryQuantityService.Create(entity);

                                        stockcontroll.totalkg = stockcontroll.totalkg - entity.Kg;
                                        stockcontroll.ModifyDate = DateTime.Now;
                                        _stockTrackingService.Update(stockcontroll);

                                    }
                                }
                                else
                                {
                                    if (j == data.Count)
                                    {

                                    }
                                    if (Convert.ToDecimal(value[j]) != data[j].Kg)
                                    {
                                        totalkg += Convert.ToDecimal(value[j]);
                                        totalprice += Convert.ToDecimal(value[j]) * Convert.ToDecimal(price);
                                        decimal eksi = decimal.Zero;
                                        if (data[j].Kg > Convert.ToDecimal(value[j]))//stoktan çıkaracak
                                        {
                                            eksi = data[j].Kg - Convert.ToDecimal(value[j]);
                                            data[j].Kg = Convert.ToDecimal(value[j]);
                                            data[j].ModifyDate = DateTime.Now;
                                            data[j].Totalprice= data[j].Kg * price;
                                            _factoryQuantityService.Update(data[j]);
                                            stockcontroll.totalkg = stockcontroll.totalkg + eksi;
                                            stockcontroll.ModifyDate = DateTime.Now;
                                            _stockTrackingService.Update(stockcontroll);

                                        }
                                        else
                                        {
                                            eksi = Convert.ToDecimal(value[j]) - data[j].Kg;
                                            data[j].Kg = Convert.ToDecimal(value[j]);
                                            data[j].ModifyDate = DateTime.Now;
                                            data[j].Totalprice = data[j].Kg * price;
                                            _factoryQuantityService.Update(data[j]);

                                            stockcontroll.totalkg = stockcontroll.totalkg - eksi;
                                            stockcontroll.ModifyDate = DateTime.Now;
                                            _stockTrackingService.Update(stockcontroll);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (stockcontroll == null)
                    {
                        if (Convert.ToDecimal(value[0]) > 0)
                        {
                            var message = $"{productType.Name} ürünü stockta bulunmamaktadır";
                            var factory = HttpContext.Session.GetString("factoryId");
                            HttpContext.Session.SetString("factoryError", message);
                            var result = HttpContext.Session.GetString("factoryQuantity");
                            if (result != null)
                            {
                                return Redirect("/Factory/GetFisByid?quantity=" + result);
                            }
                        }
                    }
                    else if (tpl > stockcontroll.totalkg)
                    {
                        if (Convert.ToDecimal(value[0]) > 0)
                        {
                            var message = $"{productType.Name} ürünü yeterli bulunmamaktadır. Güncelleyiniz";
                            var factory = HttpContext.Session.GetString("factoryId");
                            HttpContext.Session.SetString("factoryError", message);
                            var result = HttpContext.Session.GetString("factoryQuantity");
                            if (result != null)
                            {
                                return Redirect("/Factory/GetFisByid?quantity=" + result);
                            }
                        }
                    }
                }
            }
            HttpContext.Session.SetString("factoryError", "İşlem Başarılı");
            var results = HttpContext.Session.GetString("factoryQuantity");
            return Redirect("/Factory/GetFisByid?quantity=" + results);
        }
        [HttpGet]
        public IActionResult GetBeforeReceiptByFactoryid(string id)
        {
            var factoryid = CommondMethod.ConvertDecrypt(id);
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            HttpContext.Session.SetString("fabrikaid", factoryid.ToString());
            var quantitys = _receiptService.GetReceiptByCustomerId(Convert.ToInt32(factoryid), Convert.ToInt32(keys.branchid), false);

            var quantityview = new List<FactoryQuantityViewModel>();
            foreach (var item in quantitys)
            {
                var quantity = new FactoryQuantityViewModel
                {
                    createdate = item.ReceiptDate,
                    id = item.id,
                    quantity = item.DailyTakeCount
                };
                quantityview.Add(quantity);
            }
            quantityview = quantityview.OrderByDescending(x => x.createdate).OrderBy(x => x.quantity).ToList();
            return View(new AllLayoutViewModel
            {
                factoryQuantityViewModels = quantityview,
                TokenKeys = keys,
            });
        }
        [HttpGet]
        public IActionResult GetFisByid(string quantity, DateTime date)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            HttpContext.Session.SetString("factoryQuantity", quantity);
            quantity = CommondMethod.ConvertDecrypt(quantity);
            string[] val = quantity.Split(',');
            quantity = val[0];
            var date1 = val[1];
            string[] date2 = date1.Split('-');
            date = new DateTime(Convert.ToInt32(date2[0]), Convert.ToInt32(date2[1]), Convert.ToInt32(date2[2]));
            var id = HttpContext.Session.GetString("fabrikaid");
            HttpContext.Session.SetString("factoryId", id);
            var update = new ProductTypeUpdateDataModel
            {
                customerid = Convert.ToInt32(id),
                date = date,
                quantity = Convert.ToInt32(quantity)
            };
            var json = JsonConvert.SerializeObject(update);
     

            var list = _receiptService.GetReceiptByCustomerId(Convert.ToInt32(id), Convert.ToInt32(keys.branchid), false);

            var receipt = list.Where(x => x.ReceiptDate.Year == date.Year && x.ReceiptDate.Month == date.Month && x.ReceiptDate.Day == date.Day && x.DailyTakeCount == Convert.ToInt32(quantity)).FirstOrDefault();
            var d = _factoryQuantityService.GetFactoryQuantitiesByFactoryid(receipt.id).ToList();
            var s = d.Sum(x => x.Totalprice);
            ViewBag.totalprice = s;
            ViewBag.quantity = quantity;
            ViewBag.date = date.ToShortDateString();
            HttpContext.Session.SetString("updatefactory", json);
            var message = HttpContext.Session.GetString("factoryError");
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.error = message;
                return View(new AllLayoutViewModel
                {
                    TokenKeys = keys,
                    factoryQuantities = d,
                    productTypes = _productTypeService.GetAll(),

                });
            }


            return View(new AllLayoutViewModel
            {
                factoryQuantities = d,
                productTypes = _productTypeService.GetAll(),
                TokenKeys = keys
            });

        }
        [HttpGet]
        public IActionResult FullPaid(string quantity, DateTime date)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            quantity = CommondMethod.ConvertDecrypt(quantity);
            string[] val = quantity.Split(',');
            int receiptId = Convert.ToInt32(val[0]);
            decimal amaountPaid = decimal.Zero;
            var receipt = _paymentContService.GetPaymentByPay(receiptId, false);
            if (receipt != null)
            {
                amaountPaid = receipt.TotalPrice;
            }
            quantity = val[1];
            val = val[2].Split('-');
            var datetime = new DateTime(Convert.ToInt32(val[0]), Convert.ToInt32(val[1]), Convert.ToInt32(val[2]));
            var customer = HttpContext.Session.GetString("fabrikaid");
            var paymentlist = _factoryQuantityService.GetFactoryQuantitiesByFactoryid(receiptId);
            if (paymentlist != null)
            {
                var entity = new ProductQauntityViewModel
                {
                    createdate = datetime,
                    quantity = Convert.ToInt32(quantity),
                    id = Convert.ToInt32(customer),
                    branchid = Convert.ToInt32(keys.branchid),
                    receiptId = receiptId,
                };
                var json = JsonConvert.SerializeObject(entity);
                HttpContext.Session.SetString("factoryentity", json);
            }

            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
                PaymentViewModel = new PaymentViewModel
                {
                    totalPrice = Convert.ToDouble( paymentlist.Sum(x => x.Totalprice)),
                    unPaid = Convert.ToDouble(paymentlist.Sum(x => x.Totalprice) - amaountPaid),
                    amountPaid = Convert.ToDouble(amaountPaid)
                }
            }); ;
        }
        [HttpPost]
        public IActionResult FullPaid(decimal deger)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var gelenprodutc = HttpContext.Session.GetString("factoryentity");
            var value = Convert.ToDecimal(deger);
            var deserilazeproduct = JsonConvert.DeserializeObject<ProductQauntityViewModel>(gelenprodutc);
            var datetime = new DateTime(deserilazeproduct.createdate.Year, deserilazeproduct.createdate.Month, deserilazeproduct.createdate.Day);
            var payment = _paymentContService.GetPaymentByPay(deserilazeproduct.receiptId, false);
            var data = _factoryQuantityService.GetFactoryQuantitiesByFactoryid(deserilazeproduct.receiptId);
            int success = 0;
            if (payment != null)
            {
                decimal sum = payment.TotalPrice + value;
                if (value >= 1 && sum <= data.Sum(x => x.Totalprice) && payment.IsPaid == false)
                {
                    payment.TotalPrice += value;
                    var total = data.Sum(x => x.Totalprice);
                    if (payment.TotalPrice == total)
                    {
                        payment.IsPaid = true;
                    }
                    _paymentContService.Update(payment);
                    success = 1;
                }
                else if (payment.IsPaid == true)
                {
                    ViewBag.success = "Borç Bulunmamaktadır. Ödeme Tamamlanmıştır";
                    return View(new AllLayoutViewModel
                    {
                        TokenKeys = keys,
                        paymentCont = payment,
                        PaymentViewModel = new PaymentViewModel
                        {
                            totalPrice = Convert.ToDouble(data.Sum(x => x.Totalprice)),
                            unPaid = Convert.ToDouble(data.Sum(x => x.Totalprice) - payment.TotalPrice),
                            amountPaid = Convert.ToDouble(payment.TotalPrice)
                        }
                    });
                }
                else
                {
                    ViewBag.error = "Girilen değer verilecek olan ücretten büyük olamaz";
                    return View(new AllLayoutViewModel
                    {
                        TokenKeys = keys,
                        paymentCont = payment,
                        PaymentViewModel = new PaymentViewModel
                        {
                            totalPrice = Convert.ToDouble(data.Sum(x => x.Totalprice)),
                            unPaid = Convert.ToDouble(data.Sum(x => x.Totalprice) - payment.TotalPrice),
                            amountPaid = Convert.ToDouble(payment.TotalPrice)
                        }
                    });

                }

            }
            else
            {
                var total = Convert.ToDouble(data.Sum(x => x.Totalprice));
                if (value >= 1 && Convert.ToDouble(value) <= total)
                {
                    var entity = new PaymentCont
                    {
                        TotalPrice = value,
                        CreatedDate = DateTime.Now,
                        ReceiptId = deserilazeproduct.receiptId,
                        İsDelete = false,
                        ModifyDate = DateTime.Now,
                        IsPaid = false,
                        IsWhat = false
                    };

                    if (Convert.ToDouble(entity.TotalPrice) == total)
                    {
                        entity.IsPaid = true;
                    }

                    payment = _paymentContService.Create(entity);
                    success = 1;
                }
                else
                {
                    ViewBag.error = "Girilen değer verilecek olan ücretten büyük olamaz";
                    return View(new AllLayoutViewModel
                    {
                        TokenKeys = keys,
                        paymentCont = payment,
                        PaymentViewModel = new PaymentViewModel
                        {
                            totalPrice = Convert.ToDouble(data.Sum(x => x.Totalprice)),
                            unPaid = Convert.ToDouble(data.Sum(x => x.Totalprice)),
                            amountPaid = 0
                        }
                    });
                }
                if (success == 1)
                {
                    payment = _paymentContService.GetPaymentByPay(deserilazeproduct.receiptId, false);
                    ViewBag.success = "İşlem Başarılı";
                    return View(new AllLayoutViewModel
                    {
                        TokenKeys = keys,
                        paymentCont = payment,
                        PaymentViewModel = new PaymentViewModel
                        {
                            totalPrice = Convert.ToDouble(data.Sum(x => x.Totalprice)),
                            unPaid = Convert.ToDouble(data.Sum(x => x.Totalprice) - payment.TotalPrice),
                            amountPaid = Convert.ToDouble(payment.TotalPrice)
                        }
                    });
                }
            }
            ViewBag.success = "İşlem Başarısız";
            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
                paymentCont = payment,
                PaymentViewModel = new PaymentViewModel
                {
                    totalPrice = Convert.ToDouble(data.Sum(x => x.Totalprice)),
                    unPaid = Convert.ToDouble(data.Sum(x => x.Totalprice) - (payment == null ? 0 : payment.TotalPrice)),
                    amountPaid = Convert.ToDouble(payment == null ? 0 : payment.TotalPrice),
                }
            });
        }
        [HttpGet]
        public List<FactoryQuantity> GetQuantityByTypeid(string typeid)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            var list = _factoryQuantityService.GetQuantitiesByTypeid(Convert.ToInt32(typeid), Convert.ToInt32(keys.branchid)).ToList();
            return list;
            //tarihler manuel olacak YAPILDI
            //fabrikalar kısmında ürünlerle ürüm miktarları uyuşmamazlık YAPILDI
        }
        [HttpPost]
        public List<FactoryStockControllViewModel> Deneme(string quantity, DateTime date)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);

            string[] val = quantity.Split(',');
            quantity = val[0];
            var date1 = val[1];
            string[] date2 = date1.Split('-');
            var datetime = new DateTime(Convert.ToInt32(date2[0]), Convert.ToInt32(date2[1]), Convert.ToInt32(date2[2]));
            var id = HttpContext.Session.GetString("fabrikaid");
            var list = _receiptService.GetReceiptByCustomerId(Convert.ToInt32(id), Convert.ToInt32(keys.branchid), false);
            var dailey = Convert.ToInt32(quantity);
            var receipt = list.Where(x => x.ReceiptDate.Year == datetime.Year && x.ReceiptDate.Month == datetime.Month && x.ReceiptDate.Day == datetime.Day && x.DailyTakeCount == dailey).FirstOrDefault();

            var factorylist = _factoryQuantityService.GetFactoryQuantitiesByFactoryid(receipt.id);
            var factorylistgroup = factorylist.GroupBy(x => x.Name);
            var groupProductType = (from d1 in factorylist
                                    group d1 by new { d1.factoryproducttypeid } into grp
                                    select new
                                    {
                                        grp.Key.factoryproducttypeid
                                    }).ToList();
            var deneme = groupProductType.Select(x => x.factoryproducttypeid).ToList();

            var factoryquantity = new FactoryStockControllViewModel();
            var factoryquantityList = new List<FactoryStockControllViewModel>();
            var quantityFactory = new List<FactoryQuantity>();
            var productType = _productTypeService.AllProductTypeList();
            var typeList = productType.Select(x => x.id).ToList();
            var totalMoney = 0.0;
            foreach (var item in typeList)
            {
                if (deneme.Contains(item) == false)
                {
                    deneme.Add(item);
                }
            }
            quantityFactory = new List<FactoryQuantity>();
            foreach (var item in deneme)
            {
                quantityFactory = new List<FactoryQuantity>();
                factoryquantity = new FactoryStockControllViewModel();
                var data = factorylist.Where(x => x.factoryproducttypeid == item).ToList();

                if (data.Count > 0)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        if (i > data.Count - 1)
                        {
                            var product = new FactoryQuantity
                            {
                                Kg = 0,
                            };
                            quantityFactory.Add(product);
                        }
                        else
                        {
                            var product = new FactoryQuantity
                            {
                                Totalprice = data[i].Kg * data[i].UnitPrice,
                                Kg = data[i].Kg,
                                ProductPrice = data[i].ProductPrice,
                            };
                            quantityFactory.Add(product);
                        }

                    }
                    factoryquantity.productId = productType.FirstOrDefault(x => x.id == item).id;
                    factoryquantity.name = String.Concat(productType.FirstOrDefault(x => x.id == item).Name, ",", data.Select(x => x.UnitPrice).FirstOrDefault());
                    factoryquantity.total = data.Sum(x => x.Totalprice);
                    factoryquantity.price = data.Select(x => x.UnitPrice).FirstOrDefault();
                    factoryquantity.factoryQuantities = quantityFactory;
                }
                else
                {
                    var productPrice = _productPricesService.GetPriceByProductId(item, false);
                    factoryquantity.productId = productType.FirstOrDefault(x => x.id == item).id;
                    factoryquantity.name = String.Concat(productType.FirstOrDefault(x => x.id == item).Name, ",", productPrice.Price);
                    factoryquantity.total = 0;
                    factoryquantity.price = Convert.ToDecimal(productPrice.Price);
                    factoryquantity.factoryQuantities = new List<FactoryQuantity>();
                }
                factoryquantityList.Add(factoryquantity);
            }
            ViewBag.totalMoney = totalMoney;
            return factoryquantityList.OrderBy(x => x.productId).ToList();
        }
        [HttpGet]
        public IActionResult SetPriceByProductId(int id)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var data = _productTypeService.GetById(id);
            if (data == null)
            {
                ViewBag.error = "Ürün Bulunamadı";
                return View();
            }
            var price = _productPricesService.GetPriceByProductId(id, false);

            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
                productPriceByProduct = new ProductPriceByProduct
                {
                    Price = price != null ? price.Price.Replace(",", ".") : "0",
                    Name = data.Name
                }
            });
        }
        [HttpPost]
        public IActionResult SetPriceByProductId(AllLayoutViewModel request, int i)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var id = HttpContext.Request.RouteValues["id"];
            var price = _productPricesService.GetPriceByProductId(Convert.ToInt32(id), false);
            if (price == null)
            {
                var entity = new ProductPrices
                {
                    IsWhat = false,
                    Price = request.productPriceByProduct.Price.Replace(",", "."),
                    ProductId = Convert.ToInt32(id),
                };
                _productPricesService.Create(entity);
                return RedirectToAction("List", "ProductType");
            }
            if (request.productPriceByProduct.Price != price.Price)
            {
                price.İsDelete = true;
                _productPricesService.Update(price);
                price.Id = 0;
                price.Price = request.productPriceByProduct.Price.Replace(",", ".");
                price.İsDelete = false;
                _productPricesService.Create(price);
            }


            return RedirectToAction("List", "ProductType");


        }

        [HttpPost]
        public List<string> ProductList()
        {
            var products = _productTypeService.GetAll();
            var query = from d1 in products.Where(x => x.İsDelete == false)
                        join d2 in _productPricesService.GetAll() on d1.id equals d2.ProductId
                        where d2.İsDelete == false && d2.IsWhat == false
                        select new
                        {
                            Name = d1.Name,
                            Price = d2.Price,
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
    }
}