using DynamicSun_weather.Data.Interfaces;
using DynamicSun_weather.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Diagnostics;
using System.Drawing.Printing;
using PagedList;
using DynamicSun_weather.Common;
using MathNet.Numerics.Distributions;
using DynamicSun_weather.Data;

namespace DynamicSun_weather.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEntryService _entryService;
        private List<EntryModel> entries;


        public HomeController(ILogger<HomeController> logger, IEntryService entryService)
        {
            _entryService = entryService;
            _logger = logger;
            entries = new List<EntryModel>()
            {
                new EntryModel()
                {ID=1, Humidity=100 }
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadFile()
        {
            return View();
        }

        async public Task<IActionResult> ViewTable2(int? pageNumber, int? selectedYear, Month? selectedMonth, Month? currentMonth, int? currentYear)
        {

            if (selectedMonth != null || selectedYear != null)
            {
                pageNumber = 1;
            }
            else
            {
                selectedMonth = currentMonth;
                selectedYear = currentYear;
            }

            ViewData["CurrentMonth"] = selectedMonth;
            ViewData["CurrentYear"] = selectedYear;

            var pageSize = 20;
            var entr = _entryService.GetListPage(pageSize, pageNumber, selectedYear, (int?)selectedMonth +1);

            return View(await PaginatedList<EntryModel>.CreateAsync(entr, pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> UploadMultiple(ICollection<IFormFile> postedFiles)
        {
            try
            {
                await _entryService.Upload(postedFiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ViewBag.ErrorMessage = "Ошибка загрузки файла";
            }

            //_entryService.FileToEntries(postedFiles.First());

            return View("~/Views/Home/UploadFile.cshtml");
        }
    }
}