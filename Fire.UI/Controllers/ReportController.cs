using Fire.Business.Abstrack;
using Fire.Entity.Concrete;
using Fire.UI.Models;
using Fire.UI.Models.AllViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static Fire.UI.Models.AuthorizationCont;

namespace Fire.UI.Controllers
{
    public class ReportController : Controller
    {
        private readonly IProductQuantityService _productQuantityService;
        private readonly IFactoryQuantityService _factoryQuantityService;
        private readonly IPaymentContService _paymentContService;
        private readonly IReceiptService _receiptService;
        public ReportController(IProductQuantityService productQuantityService, IPaymentContService paymentContService, IFactoryQuantityService factoryQuantityServic, IReceiptService receiptService)
        {
            _factoryQuantityService = factoryQuantityServic;
            _productQuantityService = productQuantityService;
            _receiptService = receiptService;
            _paymentContService = paymentContService;
        }

        [HttpGet]
        public IActionResult DailyReport()
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
        public IActionResult DailyReport(AllLayoutViewModel model)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var receiptSerive = _receiptService.GetReceiptBeetwenDate(model.productQuantity.CreatedDate, Convert.ToInt32(keys.branchid), true, model.productQuantity.ModifyDate);

            if (receiptSerive.Count() > 0)
            {
                receiptSerive.GroupBy(x => x.ReceiptDate).Select(y => new
                {
                    ReceiptDate = y.Key,
                    id = y.Select(x => x.id).FirstOrDefault()
                }).ToList();
                var receiptList = receiptSerive.Select(x => new Receipt
                {
                    ReceiptDate = x.ReceiptDate,
                    id = x.id
                }).ToList();

                var firstdate = receiptList.OrderByDescending(x => x.ReceiptDate).LastOrDefault();
                var lastdate = receiptList.OrderByDescending(x => x.ReceiptDate).ToList()[0].ReceiptDate;


                var productQuantity = _productQuantityService.GetQuantityBeetwenDate(firstdate.ReceiptDate, lastdate);

                //toplam sermaya
                ViewBag.totalPrice = productQuantity.Sum(x => x.Totalprice);


                var group = productQuantity.GroupBy(x => x.Name).Select(y => new ProductQuantity
                {
                    Name = y.Key,
                    Totalkg = y.Sum(x => x.Kg),
                    Totalprice = y.Sum(x => x.Totalprice),
                    ReceiptId = y.First().ReceiptId
                }).ToList();

                group = group.OrderByDescending(x => x.Totalkg).ToList();

                var receipt = group.GroupBy(x => x.ReceiptId).Select(x => new Receipt
                {
                    id = x.Key
                }).ToList();

                //foreach (var item in receipt)
                //{
                //    var totalprice=_paymentContService.getpa)
                //}
                




                return View(new AllLayoutViewModel
                {
                    productQuantities = group,
                    TokenKeys=keys
                });



            }

            return View(new AllLayoutViewModel
            {
                productQuantities = new List<ProductQuantity>(),
                TokenKeys=keys
            });
        }
        [HttpGet]
        public IActionResult FactoryReport()
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
        public IActionResult FactoryReport(AllLayoutViewModel model)
        {
            //var Authorization = HttpContext.Session.GetString("token");
            //TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            //if (keys == null)
            //    return Redirect("/Error/401");
            //var value = _factoryQuantityService.GetMostReportByDate(model.Factory.CreatedDate, model.Factory.ModifyDate, Convert.ToInt32(keys.branchid));
            //var quantityGroups = value.GroupBy(x => x.Name);
            //var quantityList = new List<FactoryQuantity>();
            //foreach (var item in quantityGroups)
            //{
            //    decimal totalkg = 0;
            //    int customerid = 0;
            //    foreach (var key in item)
            //    {
            //        totalkg += key.Kg;
            //        customerid = key.factoryid;
            //    }
            //    var entity = new FactoryQuantity
            //    {
            //        Name = item.Key,
            //        Totalkg = totalkg,
            //        factoryid = customerid
            //    };
            //    quantityList.Add(entity);
            //}
            //var val = quantityList.OrderByDescending(x => x.Totalkg).Select(x => new FactoryQuantity
            //{
            //    Totalkg = x.Totalkg,
            //    Name = x.Name,
            //    factoryid = x.factoryid
            //}).ToList();
            //return View(new AllLayoutViewModel
            //{
            //    factoryQuantities = val
            //});
            return null;
        }
    }
}
