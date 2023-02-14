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
        private readonly ICustomerService _customerService;
        private readonly IExpenseDetailService _expenseDetailService;
        public ReportController(IProductQuantityService productQuantityService, IExpenseDetailService expenseDetailService,
            IPaymentContService paymentContService, ICustomerService customerService, IFactoryQuantityService factoryQuantityServic, IReceiptService receiptService)
        {
            _factoryQuantityService = factoryQuantityServic;
            _productQuantityService = productQuantityService;
            _receiptService = receiptService;
            _customerService = customerService;
            _paymentContService = paymentContService;
            _expenseDetailService = expenseDetailService;
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
                    ReceiptId = y.First().ReceiptId,
                    CreatedDate = y.First().CreatedDate
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
                    TokenKeys = keys
                });



            }

            return View(new AllLayoutViewModel
            {
                productQuantities = new List<ProductQuantity>(),
                TokenKeys = keys
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
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var receiptSerive = _receiptService.GetReceiptBeetwenDate(model.Factory.CreatedDate, Convert.ToInt32(keys.branchid), false, model.Factory.ModifyDate);
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

                var productQuantity = _factoryQuantityService.GetMostReportByDate(model.Factory.CreatedDate, model.Factory.ModifyDate, 0);
                var productQuantities = _productQuantityService.GetQuantityBeetwenDate(model.Factory.CreatedDate, model.Factory.ModifyDate);

                var customerReceiptGroup = productQuantities.GroupBy(x => x.ReceiptId).Select(y => new FactoryQuantity
                {
                    ReceiptId = y.Key,
                    Totalprice = y.Sum(x => x.Totalprice)
                }).ToList();


                var group = productQuantity.GroupBy(x => x.Name).Select(y => new ProductQuantity
                {
                    Name = y.Key,
                    Totalkg = y.Sum(x => x.Kg),
                    Totalprice = y.Sum(x => x.Totalprice),
                    ReceiptId = y.First().ReceiptId,
                    CreatedDate = y.First().CreatedDate
                }).ToList();

                group = group.OrderByDescending(x => x.Totalkg).ToList();

                var receipt = group.GroupBy(x => x.ReceiptId).Select(x => new Receipt
                {
                    id = x.Key
                }).ToList();
                //Top.Kazanç
                decimal Earnings = decimal.Zero;
                decimal EarningsTotal = decimal.Zero;
                var factoryPaymentCont = _paymentContService.GetPaymentByIsWhat(false);

                foreach (var receiptItem in receipt)
                {
                    var data = factoryPaymentCont.FirstOrDefault(x => x.ReceiptId == receiptItem.id);
                    if (data != null)
                    {
                        Earnings += data.TotalPrice;
                        EarningsTotal += group.Where(x => x.ReceiptId == receiptItem.id).ToList().Sum(x => x.Totalprice);
                        EarningsTotal = EarningsTotal - Earnings;
                    }
                    else
                    {
                        EarningsTotal += group.Where(x => x.ReceiptId == receiptItem.id).ToList().Sum(x => x.Totalprice);
                    }
                }


                ViewBag.Earnings = Earnings;
                ViewBag.EarningsTotal = EarningsTotal;
                //Top.Sermaye
                decimal Capital = decimal.Zero;
                decimal CapitalTotal = decimal.Zero;
                var customerPaymentCont = _paymentContService.GetPaymentByIsWhat(true);

                foreach (var receiptItem in customerReceiptGroup)
                {
                    var data = customerPaymentCont.FirstOrDefault(x => x.ReceiptId == receiptItem.ReceiptId);
                    if (data is not null)
                    {
                        Capital += data.TotalPrice;
                        CapitalTotal += customerReceiptGroup.Where(x => x.ReceiptId == receiptItem.ReceiptId).FirstOrDefault().Totalprice;
                        CapitalTotal = CapitalTotal - Capital;
                    }
                    else
                    {
                        CapitalTotal += customerReceiptGroup.Where(x => x.ReceiptId == receiptItem.ReceiptId).FirstOrDefault().Totalprice;
                    }

                }
                var earning = _expenseDetailService.GetExpenseByDateAndBranchId(model.Factory.CreatedDate, model.Factory.ModifyDate, Convert.ToInt32(keys.branchid));
                ViewBag.expenseDetailSum = earning.Sum(x => x.Price);
                ViewBag.Capital = Capital;
                ViewBag.CapitalTotal = CapitalTotal;

                Capital = Capital + earning.Sum(x => x.Price);
                ViewBag.SumResult = Earnings - Capital;
                ViewBag.WaitSumResult = EarningsTotal - CapitalTotal;
                return View(new AllLayoutViewModel
                {
                    productQuantities = group,
                    TokenKeys = keys
                });

            }
            return View(new AllLayoutViewModel
            {
                productQuantities = new List<ProductQuantity>(),
                TokenKeys = keys
            });
        }
    }
}
