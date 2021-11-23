using ManagementWebAppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ManagementWebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        public JsonResult getAllDevices() {
            List<MyDevice> devices = new List<MyDevice>();
            devices.Add(new MyDevice() { id = "1", text = "Dev1" });
            devices.Add(new MyDevice() { id = "2", text = "Dev2" });
            devices.Add(new MyDevice() { id = "3", text = "Dev3" });
            devices.Add(new MyDevice() { id = "4", text = "Dev4" });
            devices.Add(new MyDevice() { id = "5", text = "Dev5" });
            devices.Add(new MyDevice() { id = "6", text = "Dev6" });

            return Json(devices);
        }
    }
}