using Fire.Business.Abstrack;
using Fire.UI.Models;
using Fire.UI.Models.AllViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Fire.UI.Models.AuthorizationCont;

namespace Fire.UI.Controllers
{
    public class FactoryProductTypeController : Controller
    {
        private readonly IProductTypeService _productTypeService;
        public FactoryProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        public IActionResult List()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            return View(new AllLayoutViewModel
            {
                productTypes = _productTypeService.GetAllForFactory(),
                TokenKeys = keys,
            });
        }
    }
}
