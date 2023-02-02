using Fire.Business.Abstrack;
using Fire.UI.Models;
using Fire.UI.Models.AllViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using static Fire.UI.Models.AuthorizationCont;

namespace Fire.UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IReceiptService _receiptService;
        private readonly IProductQuantityService _productQuantityService;
       
        public DashboardController(IReceiptService receiptService, IProductQuantityService productQuantityService)
        {
            _receiptService = receiptService;
            _productQuantityService = productQuantityService;
        }

        public IActionResult Index()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var data = _receiptService.dashboard(Convert.ToInt32(keys.companyId), Convert.ToInt32(keys.branchid));

            var stock = data.StockResponse.Sum(x => x.TotalKg);
            ViewBag.stock = stock;

            return View(new AllLayoutViewModel
            {
                DashboardViewModel = data,
                TokenKeys= keys,
            });
        }
        //[HttpPost]
        //public JsonResult stockCount()
        //{
        //    var Authorization = HttpContext.Session.GetString("token");
        //    TokenKeys keys = AuthorizationCont.Authorization(Authorization);
        //    var data = _receiptService.dashboard(Convert.ToInt32(keys.branchid));

        //    var ds = Json(data.StockResponse);
        //    return ds;



        //}
    }
}
