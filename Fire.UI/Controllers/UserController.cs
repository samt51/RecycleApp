using Fire.Business.Abstrack;
using Fire.Business.Encryption;
using Fire.UI.Models;
using Fire.UI.Models.AllViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Fire.UI.Models.AuthorizationCont;

namespace Fire.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult MainPage()
        {
        
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AllLayoutViewModel allLayoutViewModel)
        {
            allLayoutViewModel.User.password = CommondMethod.ConvertToEncrypt(allLayoutViewModel.User.password);
            var result = _userService.GetUserByLogin(allLayoutViewModel.User);
            if (result != null)
            {
                var token = _userService.GetToken(result);
                HttpContext.Session.SetString("token", token.ToString());
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.error = "Kullanıcı Adınız Veya Şifreniz Yanlıştır.";
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            var Authorization = HttpContext.Session.GetString("token");
            HttpContext.Session.SetString("token", "");
            return RedirectToAction("MainPage");
        }
        [HttpGet]
        public IActionResult Detail()
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var user = _userService.GetById(Convert.ToInt32(keys.userid));
            user.password = CommondMethod.ConvertDecrypt(user.password);
         
            HttpContext.Session.SetString("userkey", keys.userid);
            return View(new AllLayoutViewModel
            {
                User = user,
                TokenKeys=keys
            });
        }
        [HttpPost]
        public IActionResult Detail(AllLayoutViewModel model)
        {
            var Authorization = HttpContext.Session.GetString("token");
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            if (keys == null)
                return Redirect("/Error/401");
            var userget = HttpContext.Session.GetString("userkey");
            var use = _userService.GetById(Convert.ToInt32(userget));
            use.email = model.User.email;
            use.password = CommondMethod.ConvertToEncrypt(model.User.password);
            use.ModifyDate = DateTime.Now;
            _userService.Update(use);
            return RedirectToAction("Detail");
        }
    }
}
