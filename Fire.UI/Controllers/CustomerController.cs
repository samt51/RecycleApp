using Fire.Business.Abstrack;
using Fire.Business.Encryption;
using Fire.DataAccess.DbConnection;
using Fire.Entity.Concrete;
using Fire.UI.Filter;
using Fire.UI.Models;
using Fire.UI.Models.AllViewModels;
using Fire.UI.Models.CustomerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using static Fire.UI.Models.AuthorizationCont;

namespace Fire.UI.Controllers
{

    //[ServiceFilter(typeof(AuthorizationAttribute))]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IProductQuantityService _productQuantityService;
        private readonly IProductTypeService _productTypeService;
        private readonly IPaymentContService _paymentContService;
        private readonly IReceiptService _receiptService;
        private readonly IProductPricesService _productPricesService;
        public CustomerController(IProductPricesService productPricesService, IPaymentContService paymentContService, IProductQuantityService productQuantityService, IProductTypeService productTypeService, ICustomerService customerService, IReceiptService receiptService)
        {
            _productPricesService = productPricesService;
            _customerService = customerService;
            _productTypeService = productTypeService;
            _productQuantityService = productQuantityService;
            _paymentContService = paymentContService;
            _receiptService = receiptService;
        }
        public IActionResult List()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            ////dolu sessionu deserialize yapmak
            //var gelenprodutc = HttpContext.Session.GetString("sessionProductType");
            //var deserilazeproduct = JsonConvert.DeserializeObject<List<ProductType>>(gelenprodutc);
            ViewBag.results = _customerService.GetAll().Where(x => x.İsDelete == false);
            return View(new AllLayoutViewModel
            {
                ListCustomer = _customerService.GetAll().Where(x => x.İsDelete == false).ToList(),
                TokenKeys = keys

            });
        }
        public IActionResult Add()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            return View(new AllLayoutViewModel
            {
                TokenKeys = keys
            });
        }
        [HttpPost]
        public IActionResult Add(AllLayoutViewModel model)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");

            var entity = new Customer
            {
                Surname = model.CustomerAdd.Surname,
                CreatedDate = System.DateTime.Now,
                ModifyDate = System.DateTime.Now,
                Name = model.CustomerAdd.Name,
                PhoneNumber = model.CustomerAdd.PhoneNumber,
                İsDelete = false,
                companyId = Convert.ToInt32(keys.branchid)
            };
            _customerService.Create(entity);
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Update(string id)
        {
            var customerid = CommondMethod.ConvertDecrypt(id);
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var value = _customerService.GetById(Convert.ToInt32(customerid));
            return View(new AllLayoutViewModel
            {
                Customer = value,
                TokenKeys = keys
            });
        }
        [HttpPost]
        public IActionResult Update(AllLayoutViewModel model)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var id = HttpContext.Request.RouteValues["id"];
            id = CommondMethod.ConvertDecrypt(id.ToString());
            var deger = _customerService.GetById(Convert.ToInt32(id));
            deger.Name = model.Customer.Name;
            deger.PhoneNumber = model.Customer.PhoneNumber;
            deger.Surname = model.Customer.Surname;
            deger.ModifyDate = System.DateTime.Now;
            _customerService.Update(deger);
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var customer = _customerService.GetById(id);
            customer.İsDelete = true;
            _customerService.Update(customer);
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult GetBeforeFisWithCustomerid(string id)
        {
            var customerid = CommondMethod.ConvertDecrypt(id);
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            HttpContext.Session.SetString("customerid", customerid.ToString());
            var quantitys = _receiptService.GetReceiptByCustomerId(Convert.ToInt32(customerid), Convert.ToInt32(keys.branchid), true);

            var quantityview = new List<ProductQauntityViewModel>();
            foreach (var item in quantitys)
            {
                var quantity = new ProductQauntityViewModel
                {
                    createdate = item.ReceiptDate,
                    id = item.id,
                    quantity = item.DailyTakeCount
                };
                quantityview.Add(quantity);

            }
            quantityview = quantityview.OrderByDescending(x => x.createdate).ToList();
            var error = HttpContext.Session.GetString("error1");
            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.error = error;
            }
            return View(new AllLayoutViewModel
            {
                productQauntityViewModels = quantityview,
                TokenKeys = keys
            });

        }
        [HttpGet]
        public IActionResult GetFisByid(string quantity, DateTime date)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            quantity = CommondMethod.ConvertDecrypt(quantity);
            string[] val = quantity.Split(',');
            quantity = val[0];
            string date1 = val[1];
            string[] date2 = date1.Split('-');
            date = new DateTime(Convert.ToInt32(date2[0]), Convert.ToInt32(date2[1]), Convert.ToInt32(date2[2]));
            var customer = HttpContext.Session.GetString("customerid");

            var update = new ProductTypeUpdateDataModel
            {

                customerid = Convert.ToInt32(customer),
                date = date,
                quantity = Convert.ToInt32(quantity)
            };
            var json = JsonConvert.SerializeObject(update);
            HttpContext.Session.SetString("updateJson", json);
            var list = _receiptService.GetReceiptByCustomerId(Convert.ToInt32(customer), Convert.ToInt32(keys.branchid), true);
            var reciptData = list.Where(x => x.ReceiptDate.Year == date.Year && x.ReceiptDate.Month == date.Month && x.ReceiptDate.Day == date.Day && x.DailyTakeCount == Convert.ToInt32(quantity)).FirstOrDefault();
            if (reciptData == null)
            {
                string errordata = "Fiş Bulunamadı";
                HttpContext.Session.SetString("error1", errordata);
            }
            var productQuantity = _productQuantityService.GetQuantityByReceiptBy(reciptData.id);
            var sum = productQuantity.Sum(x => x.Totalprice);
            ViewBag.totalprice = sum;
            ViewBag.quantity = quantity;
            ViewBag.date = date.ToShortDateString();
            var error = HttpContext.Session.GetString("errorMessage");
            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.error = error;
            }
            return View(new AllLayoutViewModel
            {
                productQuantities = productQuantity,
                productTypes = _productTypeService.GetAll(),
                TokenKeys = keys
            });

        }
        [HttpGet]
        public IActionResult FullPaid(string quantity, int a)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            quantity = CommondMethod.ConvertDecrypt(quantity);
            string[] val = quantity.Split(',');
            int receiptId = Convert.ToInt32(val[0]);
            decimal amaountPaid = decimal.Zero;
            var receipt = _paymentContService.GetPaymentByPay(receiptId, true);
            if (receipt != null)
            {
                amaountPaid = receipt.TotalPrice;
            }
            quantity = val[1];
            val = val[2].Split('-');
            var datetime = new DateTime(Convert.ToInt32(val[0]), Convert.ToInt32(val[1]), Convert.ToInt32(val[2]));
            var customer = HttpContext.Session.GetString("customerid");

            var paymentlist = _productQuantityService.GetQuantityByReceiptBy(receiptId);
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
                HttpContext.Session.SetString("entity", json);
            }

            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
                PaymentViewModel = new PaymentViewModel
                {
                    totalPrice = Convert.ToDouble(paymentlist.Sum(x => x.Totalprice)),
                    unPaid = Convert.ToDouble(paymentlist.Sum(x => x.Totalprice) - amaountPaid),
                    amountPaid = Convert.ToDouble(amaountPaid),
                }
            });
        }
        [HttpPost]
        public IActionResult FullPaid(decimal deger)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var value = Convert.ToDecimal(deger);
            var gelenprodutc = HttpContext.Session.GetString("entity");
            var deserilazeproduct = JsonConvert.DeserializeObject<ProductQauntityViewModel>(gelenprodutc);
            var datetime = new DateTime(deserilazeproduct.createdate.Year, deserilazeproduct.createdate.Month, deserilazeproduct.createdate.Day);
            var payment = _paymentContService.GetPaymentByPay(deserilazeproduct.receiptId, true);
            var data = _productQuantityService.GetQuantityByReceiptBy(deserilazeproduct.receiptId);
            int success = 0;
            if (payment != null)
            {
                decimal sum = payment.TotalPrice + value;
                if (value >= 1 && sum <= data.Sum(x => x.Totalprice) && payment.IsPaid == false)
                {
                    payment.TotalPrice += value;
                    var total = data.Sum(x => x.Totalprice);
                    //if (Convert.ToDouble(value) < total)
                    //{
                    //   payment.TotalPrice = total - Convert.ToDouble(value);
                    //}
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
                if (success == 1)
                {
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
                ViewBag.success = "İşlem Başarısız";
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
                        IsWhat = true
                    };

                    if (Convert.ToDouble( entity.TotalPrice )== total)
                    {
                        entity.IsPaid = true;
                    }

                    _paymentContService.Create(entity);
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
                    payment = _paymentContService.GetPaymentByPay(deserilazeproduct.receiptId, true);
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
        public List<ProductQuantity> GetQuantityByTypeid(string typeid)
        {
            var list = _productQuantityService.GetQuantitiesByTypeid(Convert.ToInt32(typeid)).ToList();
            return list;
        }
        [HttpPost]
        public List<ProductQuantityViewModel> Deneme(string quantity, DateTime date)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            string[] val = quantity.Split(',');
            quantity = val[0];
            string date1 = val[1];
            string[] date2 = date1.Split('-');
            var datetime = new DateTime(Convert.ToInt32(date2[0]), Convert.ToInt32(date2[1]), Convert.ToInt32(date2[2]));
            var customer = HttpContext.Session.GetString("customerid");
            var receiptData = _receiptService.GetReceiptByCustomerId(Convert.ToInt32(customer), Convert.ToInt32(keys.branchid), true);
            var d = receiptData.Where(x => x.ReceiptDate.Year == datetime.Year && x.ReceiptDate.Month == datetime.Month && x.ReceiptDate.Day == datetime.Day && x.DailyTakeCount == Convert.ToInt32(quantity)).FirstOrDefault();
            var productQuantity = _productQuantityService.GetQuantityByReceiptBy(d.id);
            var quantitygroup = productQuantity.GroupBy(x => x.Name).ToList();

            var groupProductType = (from d1 in productQuantity
                                    group d1 by new { d1.productTypeid } into grp
                                    select new
                                    {
                                        grp.Key.productTypeid
                                    }).ToList();

            var deneme = groupProductType.Select(x => x.productTypeid).ToList();
            var number = new List<ProductQuantity>();
            var quantityentity = new ProductQuantityViewModel();
            var quantityList = new List<ProductQuantityViewModel>();
            var productType = _productTypeService.AllProductTypeList();
            var typeList = productType.Select(x => x.id).ToList();
            foreach (var item in typeList)
            {
                if (deneme.Contains(item) == false)
                {
                    deneme.Add(item);
                }
            }


            double money = 00.0;
            var ss = new List<ProductQuantity>();
            foreach (var item in deneme)
            {
                ss = new List<ProductQuantity>();
                quantityentity = new ProductQuantityViewModel();

                var data = productQuantity.Where(x => x.productTypeid == item).ToList();
                if (data.Count > 0)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        if (i > data.Count - 1)
                        {
                            var product = new ProductQuantity
                            {
                                Kg = 0
                            };
                            ss.Add(product);
                        }
                        else
                        {
                            var product = new ProductQuantity
                            {
                                Totalprice = data[i].Totalprice,
                                Kg = data[i].Kg
                            };
                            ss.Add(product);
                        }

                    }
                    quantityentity.productId = productType.FirstOrDefault(x => x.id == item).id;
                    quantityentity.name = String.Concat(productType.FirstOrDefault(x => x.id == item).Name, ",", data.Select(x => x.UnitPrice).FirstOrDefault());
                    quantityentity.total = data.Sum(x => x.Totalprice);
                    quantityentity.price = data.Select(x => x.UnitPrice).FirstOrDefault();
                    quantityentity.productQuantities = ss;
                }
                else
                {
                    var productPrice = _productPricesService.GetPriceByProductId(item, true);
                    quantityentity.productId = productType.FirstOrDefault(x => x.id == item).id;
                    quantityentity.name = String.Concat(productType.FirstOrDefault(x => x.id == item).Name, ",", productPrice.Price);
                    quantityentity.total = 0;
                    quantityentity.price = Convert.ToDecimal(productPrice.Price);
                    quantityentity.productQuantities = new List<ProductQuantity>();
                }

                quantityList.Add(quantityentity);


            }
            ViewBag.totalMoney = money;
            return quantityList.OrderBy(x => x.productId).ToList();

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
            var price = _productPricesService.GetPriceByProductId(id, true);

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
            var price = _productPricesService.GetPriceByProductId(Convert.ToInt32(id), true);
            if (price == null)
            {
                var entity = new ProductPrices
                {
                    IsWhat = true,
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
                _productPricesService.Create(price);
            }

            return RedirectToAction("List", "ProductType");

        }
    }
}