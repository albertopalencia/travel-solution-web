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
    public class EditorialController : Controller
    {

        private readonly FunctionHelper _functionHelper;
        private readonly EndPointSetting _endpoint;

        public EditorialController(FunctionHelper functionHelper, IOptions<EndPointSetting> options)
        {
            _functionHelper = functionHelper;
            _endpoint = options.Value;
        }

        public async Task<ActionResult> Index()
        {
            var editorials = await _functionHelper.GetPagedResponse<List<EditorialModel>>(_endpoint.TravelApi, _endpoint.Editorial);
            return View(editorials.Data);
        }
        
        public ActionResult Create()
        {
            return View(new CreateEditorialViewModel());
        }

        [HttpPost] 
        public async Task<IActionResult> Create(CreateEditorialViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.Get<string>("token");
                var result =
                    await _functionHelper.PostAuthorization<Response<int>, CreateEditorialViewModel>(_endpoint.TravelApi, _endpoint.Editorial, token,model);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                ModelState.AddModelError("EditorialError", result.Message);
            }

            return View(model);
        }

    }
}
