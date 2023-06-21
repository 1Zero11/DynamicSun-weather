using DynamicSun_weather.Data;
using DynamicSun_weather.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Diagnostics;

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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ViewTable2()
        {
            entries = new List<EntryModel>(_entryService.GetList(20));

            return View(entries);
        }
        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadMultiple(ICollection<IFormFile> postedFiles)
        {
            await _entryService.Upload(postedFiles);
            //_entryService.FileToEntries(postedFiles.First());

            return Accepted(postedFiles);
        }
    }
}