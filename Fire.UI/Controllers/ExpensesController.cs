using Fire.Business.Abstrack;
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
    public class ExpensesController : Controller
    {
        private readonly IExpensesService _expensesService;
        private readonly IEmployeesService _employeesService;
        private readonly ICustomerService _customerService;
        private readonly IFactoryService _factoryService;
        private readonly IExpenseDetailService _expenseDetailService;
        private readonly IExpenseCategoriaService _expenseCategoriaService;
        public ExpensesController(IEmployeesService employeesService, IExpensesService expensesService, ICustomerService customerService, IFactoryService factoryService, IExpenseDetailService expenseDetailService, IExpenseCategoriaService expenseCategoriaService)
        {
            _expensesService = expensesService;
            _employeesService = employeesService;
            _customerService = customerService;
            _factoryService = factoryService;
            _expenseDetailService = expenseDetailService;
            _expenseCategoriaService = expenseCategoriaService;
        }
        //public IActionResult ExpenseMainPage()
        //{
        //    var Authorization = HttpContext.Session.GetString("token");
        //    TokenKeys keys = AuthorizationCont.Authorization(Authorization);
        //    if (keys == null)
        //        return Redirect("/Error/401");
        //    var customerlist = _customerService.GetAll();
        //    var factory = _factoryService.GetAll();
        //    var customerAndFactory = new List<FactoryAndCustomerViewModel>();
        //    foreach (var item in factory)
        //    {
        //        customerAndFactory.Add(new FactoryAndCustomerViewModel
        //        {
        //            id = item.id,
        //            Name = item.name,
        //            PhoneNumber = item.Phone,
        //            Type = false
        //        });
        //    }
        //    foreach (var customer in customerlist)
        //    {
        //        customerAndFactory.Add(new FactoryAndCustomerViewModel
        //        {
        //            id = customer.id,
        //            Name = customer.Name,
        //            Surname = customer.Surname,
        //            PhoneNumber = customer.PhoneNumber,
        //            Type = true
        //        });
        //    }

        //    return View(new AllLayoutViewModel
        //    {
        //        TokenKeys = keys,
        //        factoryAndCustomerViewModels = customerAndFactory
        //    });
        //}
        public IActionResult PostExpenseById(string id)
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
        public IActionResult PostExpenseById(AllLayoutViewModel model, int a)
        {
            return View();
        }
        [HttpGet]
        public IActionResult ExpenseGetList(AllLayoutViewModel model)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            var expenseCategoria = _expenseCategoriaService.GetAll();
            foreach (var item in expenseCategoria)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = item.name,
                    Value = item.id.ToString()
                });
            }
            ViewBag.expenseCategoria = selectListItems;
            var data = _expenseDetailService.GetAll(x => x.BranchId == Convert.ToInt32(keys.branchid));
            decimal sumTotal = data.Sum(x => x.Price);
            ViewBag.total = sumTotal;
            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
                ExpenseDetails = _expenseDetailService.GetAll(x => x.BranchId == Convert.ToInt32(keys.branchid)),
            });
        }
        [HttpPost]
        public IActionResult ExpenseGetList(AllLayoutViewModel model, int a)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            var expenseCategoria = _expenseCategoriaService.GetAll();
            foreach (var item in expenseCategoria)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = item.name,
                    Value = item.id.ToString()
                });
            }
            ViewBag.expenseCategoria = selectListItems;
            var data = _expenseDetailService.GetAll(x => x.BranchId == Convert.ToInt32(keys.branchid));

            decimal sumTotal = data.Sum(x => x.Price);
            ViewBag.total = sumTotal;
            ViewBag.categoritotal = data.Where(x => x.ExpenseCategoriaId == model.expenseCategoria.id).Sum(x => x.Price);
            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
                ExpenseDetails = data,
            });
        }
        [HttpGet]
        public IActionResult AddExpense()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            var expenseCategoria = _expenseCategoriaService.GetAll();
            foreach (var item in expenseCategoria)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = item.name,
                    Value = item.id.ToString()
                });
            }
            ViewBag.expenseCategoria = selectListItems;
            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
            });
        }
        [HttpPost]
        public IActionResult AddExpense(AllLayoutViewModel model, int a)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            var expenseCategoria = _expenseCategoriaService.GetAll();
            foreach (var item in expenseCategoria)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = item.name,
                    Value = item.id.ToString()
                });
            }
            ViewBag.expenseCategoria = selectListItems;

            if (model.expenseCategoria.id == 0)
            {
                ViewBag.error = "Gider Kategorisi Zorunludur.";

                return View(new AllLayoutViewModel
                {
                    TokenKeys = keys,
                    ExpenseDetail = model.ExpenseDetail
                });
            }
            if (model.ExpenseDetail.Price <= 0)
            {
                ViewBag.error = "Girilen Gider Miktarı 0'dan Büyük olmalı.";


                return View(new AllLayoutViewModel
                {
                    TokenKeys = keys,
                    ExpenseDetail = model.ExpenseDetail
                });
            }
            _expenseDetailService.Create(new Entity.Concrete.ExpenseDetail
            {
                Description = model.ExpenseDetail.Description,
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                ExpenseCategoriaId = model.expenseCategoria.id,
                Price = model.ExpenseDetail.Price,
                BranchId = Convert.ToInt32(keys.branchid),
                İsDelete = false
            });

            ViewBag.success = "İşlem Başarılı";

            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
                ExpenseDetail = model.ExpenseDetail
            });
        }
        [HttpGet]
        public IActionResult AddCategoria()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            var expenseCategoria = _expenseCategoriaService.GetAll();
            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
            });
        }
        [HttpPost]
        public IActionResult AddCategoria(AllLayoutViewModel model)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");

            var expenseCategoria = _expenseCategoriaService.GetAll().Select(x => new ExpenseCategoria
            {
                name = x.name.ToLower()
            }).ToList();

            var data = expenseCategoria.Where(x => x.name == model.expenseCategoria.name.ToLower()).FirstOrDefault();
            if (data is not null)
            {
                ViewBag.error = $"{model.expenseCategoria.name} adlı kategori mevcuttur.";
                return View(new AllLayoutViewModel
                {
                    TokenKeys = keys,
                    expenseCategoria = model.expenseCategoria
                });
            }
            _expenseCategoriaService.Create(new ExpenseCategoria
            {
                name = model.expenseCategoria.name,
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                İsDelete = false
            });
            ViewBag.success = "İşlem Başarılı";
            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
                expenseCategoria = model.expenseCategoria
            });
        }
        [HttpGet]
        public IActionResult GetCategoriaList()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            var expenseCategoria = _expenseCategoriaService.GetAll();
            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
                ExpenseCategorias = expenseCategoria
            });
        }


    }
}