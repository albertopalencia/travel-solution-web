using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelWeb.Client.Helpers;
using TravelWeb.Client.Models;
using TravelWeb.Client.Services;
using TravelWeb.Client.Settings;

namespace TravelWeb.Client.Controllers
{
    public class BookController : Controller
    {
        
        private readonly FunctionHelper _functionHelper;
        private readonly EndPointSetting _endpoint;


        public BookController(FunctionHelper functionHelper, IOptions<EndPointSetting> options)
        {
            _functionHelper = functionHelper;
            _endpoint = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _functionHelper.GetPagedResponse<List<BookModel>>(_endpoint.TravelApi, _endpoint.BookUrl);
            return View(books.Data);
        }

        public async Task<IActionResult> Create()
        {
            await LoadSelect().ConfigureAwait(false);
            return View(new CreateBookViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            if (ModelState.IsValid)
            {
              var result =  await _functionHelper.PostResponse<int, CreateBookViewModel>(_endpoint.TravelApi, _endpoint.BookUrl, model);
              if (result.Succeeded)
              {
                  return RedirectToAction("Index", "Book");
              }

              ModelState.AddModelError("Errors", result.Message);
            }

            await LoadSelect().ConfigureAwait(false);
            return View(model);
        }

        private async Task LoadSelect()
        {
            var editorials =
                await _functionHelper.GetPagedResponse<List<EditorialModel>>(_endpoint.TravelApi, _endpoint.Editorial);
            var authors =
                await _functionHelper.GetPagedResponse<List<AuthorModel>>(_endpoint.TravelApi, _endpoint.AuthorUrl);
            ViewBag.Editorials =
                new SelectList(editorials.Data.Select(s => new { s.Id, Name = string.Join(" - ", s.Name, s.Campus) }).ToList(),
                    "Id", "Name");
            ViewBag.Authors =
                new SelectList(authors.Data.Select(it => new { it.Id, Name = $"{it.Name} - {it.LastName}" }).ToList(),
                    "Id", "Name");
        }
    }
}
