using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TravelWeb.Client.Extensions;
using TravelWeb.Client.Helpers;
using TravelWeb.Client.Models;
using TravelWeb.Client.Services;
using TravelWeb.Client.Settings;
using TravelWeb.Client.Wrappers;

namespace TravelWeb.Client.Controllers
{
    public class AuthorController : Controller
    {
        private readonly FunctionHelper _functionHelper;
        private readonly EndPointSetting _endpoint;

        public AuthorController(FunctionHelper functionHelper, IOptions<EndPointSetting> options)
        {
            _functionHelper = functionHelper;
            _endpoint = options.Value;
        }


        public async Task<ActionResult> Index()
        {
            var authors = await _functionHelper.GetPagedResponse<List<AuthorModel>>(_endpoint.TravelApi, _endpoint.AuthorUrl);
            return View(authors.Data);
        }

        public ActionResult Create()
        {
            return View(new CreateAuthorViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.Get<string>("token");
                var result =
                    await _functionHelper.PostAuthorization<Response<int>, CreateAuthorViewModel>(_endpoint.TravelApi, _endpoint.AuthorUrl, token, model);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                ModelState.AddModelError("AuthorError", result.Message);
            }

            return View(model);
        }

    }
}
