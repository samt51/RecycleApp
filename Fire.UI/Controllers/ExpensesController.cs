using Fire.Business.Abstrack;
using Fire.UI.Models;
using Fire.UI.Models.AllViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static Fire.UI.Models.AuthorizationCont;

namespace Fire.UI.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpensesService _expensesService;
        private readonly IEmployeesService _employeesService;
        private readonly ICustomerService _customerService;
        private readonly IFactoryService _factoryService;
        public ExpensesController(IEmployeesService employeesService, IExpensesService expensesService, ICustomerService customerService, IFactoryService factoryService)
        {
            _expensesService = expensesService;
            _employeesService = employeesService;
            _customerService = customerService;
            _factoryService = factoryService;
        }
        public IActionResult ExpenseMainPage()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var customerlist = _customerService.GetAll();
            var factory = _factoryService.GetAll();
            var customerAndFactory = new List<FactoryAndCustomerViewModel>();
            foreach (var item in factory)
            {
                customerAndFactory.Add(new FactoryAndCustomerViewModel
                {
                    id = item.id,
                    Name = item.name,
                    PhoneNumber = item.Phone,
                    Type = false
                });
            }
            foreach (var customer in customerlist)
            {
                customerAndFactory.Add(new FactoryAndCustomerViewModel
                {
                    id = customer.id,
                    Name = customer.Name,
                    Surname = customer.Surname,
                    PhoneNumber = customer.PhoneNumber,
                    Type = true
                });
            }

            return View(new AllLayoutViewModel
            {
                TokenKeys = keys,
                factoryAndCustomerViewModels=customerAndFactory
            });
        }
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
    }
}