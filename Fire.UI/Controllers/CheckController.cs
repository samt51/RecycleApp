using Fire.Business.Abstrack;
using Fire.Business.Encryption;
using Fire.Entity.Concrete;
using Fire.UI.Models;
using Fire.UI.Models.AllViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using static Fire.UI.Models.AuthorizationCont;

namespace Fire.UI.Controllers
{
    public class CheckController : Controller
    {
        private readonly ICheckService _checkService;
        private readonly IBankService _bankService;
        private readonly ICustomerService _customerService;
        private readonly IFactoryService _factoryService;
        public CheckController(ICustomerService customerService, IBankService bankService, IFactoryService factoryService, ICheckService checkService)
        {
            _checkService = checkService;
            _bankService = bankService;
            _factoryService = factoryService;
            _customerService = customerService;
        }
        [HttpGet]
        public IActionResult GetList()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var check = _checkService.GetCheckByBranchid(Convert.ToInt32(keys.branchid));
            ViewBag.results = check;
            return View(new AllLayoutViewModel
            {
                ChecksList = check,
                TokenKeys = keys
            }); ;
        }
        [HttpGet]
        public IActionResult Add(string id)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var dataid = CommondMethod.ConvertDecrypt(id);
            string[] splited = dataid.Split(",");
            var banklist = _bankService.GetAll();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var bank in banklist)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = bank.name,
                    Value = bank.id.ToString()
                });
            }
            ViewBag.banklist = selectListItems;
            if (splited.Count() >= 3)
            {
                if (splited[2] == "error")
                {
                    ViewBag.error = splited[3];
                    return View(new AllLayoutViewModel
                    {
                        TokenKeys=keys
                    });
                }
                else
                {
                    ViewBag.success = "İşlem Başarılı";
                    return View(new AllLayoutViewModel
                    {
                        TokenKeys=keys
                    });
                }
            }
            return View(new AllLayoutViewModel
            {
                TokenKeys = keys
            });
        }
        [HttpPost]
        public IActionResult Add(AllLayoutViewModel model)
        {
            var id = HttpContext.Request.RouteValues["id"];
            var Authorization = HttpContext.Session.GetString("token");

            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");

            var dataid = CommondMethod.ConvertDecrypt(id.ToString());
            string[] splited = dataid.Split(",");
            if (model.bank.id == 0)
            {
                dataid = dataid + "," + "error" + "," + "Banka Zorunludur";
                dataid = CommondMethod.ConvertToEncrypt(dataid);
                return Redirect("/Check/Add/" + dataid);
            }
            var checkserialnumber = _checkService.GetCheckBySerialNumber(model.check.checkNumber, model.bank.id);
            if (checkserialnumber != null)
            {
                ViewBag.error = "Bu Serial Numaraya ait çek mevcuttur.";
                dataid = dataid + "," + "error" + "," + "Bu Serial Numaraya ait çek mevcuttur.";
                dataid = CommondMethod.ConvertToEncrypt(dataid);
                return Redirect("/Check/Add/" + dataid);
            }

            int success = 0;
            if (splited[1] == "false")
            {

                var entity = new Check
                {
                    bankid = model.bank.id,
                    branchid = Convert.ToInt32(keys.branchid),
                    checkDate = model.check.checkDate,
                    checkNumber = model.check.checkNumber,
                    CreatedDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    price = model.check.price,
                    toWhoWasGiven = 0,
                    whoFromGetted = Convert.ToInt32(splited[0]),
                    İsDelete = false,
                    IsWhat = false

                };
                _checkService.Create(entity);
                success = 1;
            }
            else
            {
                var entity = new Check
                {
                    bankid = model.bank.id,
                    branchid = Convert.ToInt32(keys.branchid),
                    checkDate = model.check.checkDate,
                    checkNumber = model.check.checkNumber,
                    CreatedDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    price = model.check.price,
                    toWhoWasGiven = 0,
                    whoFromGetted = Convert.ToInt32(splited[0]),
                    İsDelete = false,
                    IsWhat = true

                };
                _checkService.Create(entity);
                success = 1;


            }
            var banklist = _bankService.GetAll();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var bank in banklist)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = bank.name,
                    Value = bank.id.ToString()
                });
            }
            ViewBag.banklist = selectListItems;
            dataid = dataid + "," + "success";
            dataid = CommondMethod.ConvertToEncrypt(dataid);
            return Redirect("/Check/Add/" + dataid);
        }
        [HttpGet]
        public IActionResult CheckGive(string id)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            HttpContext.Session.SetString("checkkey", id.ToString());
            var givelist = _checkService.GetEmptyCheckByBranchId(Convert.ToInt32(keys.branchid));
            ViewBag.results = givelist;
            return View(new AllLayoutViewModel
            {
                ChecksList = givelist,
                TokenKeys=keys
            });
        }
        [HttpPost]
        public IActionResult CheckGive(int id, int a)
        {
            var customerid = HttpContext.Session.GetString("checkkey");
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var value = _checkService.GetById(id);
            value.toWhoWasGiven = Convert.ToInt32(CommondMethod.ConvertDecrypt(customerid.ToString()));
            value.ModifyDate = DateTime.Today;
            _checkService.Update(value);
            return RedirectToAction("List", "Customer");
        }
        [HttpGet]
        public IActionResult CheckOfCustomer(string id)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var customerid = CommondMethod.ConvertDecrypt(id);
            int branchid = Convert.ToInt32(keys.branchid);
            int customer = Convert.ToInt32(customerid);
            var checklist = _checkService.GetEmptyCheckByGiveNumber(branchid, customer);
            ViewBag.results = checklist;
            return View(new AllLayoutViewModel
            {
                ChecksList = checklist,
                TokenKeys=keys
            });
        }
        [HttpGet]
        public IActionResult NewCheck()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var banklist = _bankService.GetAll();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var bank in banklist)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = bank.name,
                    Value = bank.id.ToString()
                });
            }
            ViewBag.banklist = selectListItems;

            return View(new AllLayoutViewModel
            {
                TokenKeys=keys
            });
        }
        [HttpPost]
        public IActionResult NewCheck(AllLayoutViewModel model)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var banklist = _bankService.GetAll();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var bank in banklist)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = bank.name,
                    Value = bank.id.ToString()
                });
            }
            ViewBag.banklist = selectListItems;
            var checkserialnumber = _checkService.GetCheckBySerialNumber(model.check.checkNumber, model.bank.id);
            if (checkserialnumber != null)
            {
                ViewBag.error = "Bu Serial Numaraya ait çek mevcuttur.";
                return View(new AllLayoutViewModel());
            }

            var entity = new Check
            {
                bankid = model.bank.id,
                branchid = Convert.ToInt32(keys.branchid),
                checkDate = model.check.checkDate,
                checkNumber = model.check.checkNumber,
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                price = model.check.price,
                toWhoWasGiven = null,
                whoFromGetted = Convert.ToInt32(keys.branchid),
                İsDelete = false,
            };
            _checkService.Create(entity);
            return RedirectToAction("GetList");
        }
    }
}
