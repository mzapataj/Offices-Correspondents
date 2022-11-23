using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prueba.WebSites.ViewModel;
using System.Diagnostics;
using WebSites.Services.Interfaces;

namespace Common.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICorresponsalesServices _corresponsalesServices;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICorresponsalesServices corresponsalesServices)
        {
            _logger = logger;
            _corresponsalesServices = corresponsalesServices;            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CorresponsalesCountOficinas()
        {
            ViewBag.Path = _corresponsalesServices.Path;

            return View();
        }
    }
}