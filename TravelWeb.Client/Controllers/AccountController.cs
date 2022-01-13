using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TravelWeb.Client.Extensions;
using TravelWeb.Client.Helpers;
using TravelWeb.Client.Models;
using TravelWeb.Client.Models.Accounts;
using TravelWeb.Client.Settings;

namespace TravelWeb.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly FunctionHelper _functionHelper;
        private readonly EndPointSetting _endpoint;
        public AccountController(FunctionHelper functionHelper, IOptions<EndPointSetting> options)
        {
            _functionHelper = functionHelper;
            _endpoint = options.Value;
        }

        public IActionResult Signup()
        {
            var model = new SignupModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupModel model)
        {
            if (!ModelState.IsValid) return await Task.FromResult<IActionResult>(View(model));
             
            return await Task.FromResult<IActionResult>(View(model));
        }


        [HttpGet]
        public IActionResult Login(LoginModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginModel model)
        {
            if (!ModelState.IsValid) return View("Login", model);
            var result =
                await _functionHelper.PostResponse<AuthenticationResponseDto, LoginModel>(_endpoint.TravelApi,
                    _endpoint.Authenticate, model);
            if (result.Succeeded)
            {
                HttpContext.Session.Set("authenticate", true);
                HttpContext.Session.Set("userName", result.Data.UserName);
                HttpContext.Session.Set("token", result.Data.JwToken);
                HttpContext.Session.Set("AuthenticationResponse",result.Data);
                return RedirectToAction("Index", "Book");
            }

            ModelState.AddModelError("LoginError", "Email and password do not match");

            return View("Login", model);
        }

        public IActionResult Signout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}