using Microsoft.AspNetCore.Mvc;

namespace Fire.UI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult GeneralError(int statusCode)
        {
            if (statusCode == 404)
            {
                ViewBag.Title = "Not Found";
                ViewBag.ErrorCode = "404";
                ViewBag.ErrorMessage = "Page Not Found";
                return View();
            }
            else if (statusCode == 401)
            {
                ViewBag.Title = "Unauthorized Access";
                ViewBag.ErrorCode = "401";
                ViewBag.ErrorMessage = "Authorization Fail";
                return View();
            }
            else if (statusCode == 501)
            {
                ViewBag.Title = "Servicess Error";
                ViewBag.ErrorCode = "501";
                ViewBag.ErrorMessage = "Service Error";
            }
            return View();
        }
    }
}
