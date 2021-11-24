using Bachelor.Common;
using Bachelor.Common.Models;
using ManagementWebAppMVC.Models;
using ManagementWebAppMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ManagementWebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICosmosDBService _cosmosDBService;

        public HomeController(ILogger<HomeController> logger, ICosmosDBService cosmosDBService)
        {
            _logger = logger;
            _cosmosDBService = cosmosDBService;
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

        public JsonResult GetAllDevices() {
            List<DeviceViewModel> devices = new List<DeviceViewModel>();
            devices.Add(new DeviceViewModel() { id = "1", text = "subDevice_1", unitType = "Celcius" });
            devices.Add(new DeviceViewModel() { id = "2", text = "subDevice_2", unitType = "Celcius" });
            devices.Add(new DeviceViewModel() { id = "3", text = "subDevice_3", unitType = "psi" });
            devices.Add(new DeviceViewModel() { id = "4", text = "subDevice_4", unitType = "Celcius" });
            devices.Add(new DeviceViewModel() { id = "5", text = "subDevice_5", unitType = "rpm" });
            devices.Add(new DeviceViewModel() { id = "6", text = "subDevice_6", unitType = "rpm" });
            devices.Add(new DeviceViewModel() { id = "7", text = "subDevice_7", unitType = "Celcius" });
            devices.Add(new DeviceViewModel() { id = "8", text = "subDevice_8", unitType = "Celcius" });
            devices.Add(new DeviceViewModel() { id = "9", text = "subDevice_9", unitType = "psi" });
            devices.Add(new DeviceViewModel() { id = "10", text = "subDevice_10", unitType = "Celcius" });

            return Json(devices);
        }

        public async Task<IActionResult> GetAllTelemetryForDevice(string deviceId, string unitType) {
            var telemetry = await _cosmosDBService.GetItemsAsync<OpsDeviceTelemetry>(x => x.measurements.Any(y => y.subId.Equals(deviceId)));

            var telemetry1 = telemetry.Select(x => x.timestamp);
            var telemetry2 = telemetry.Select(x => x.measurements.Where(item => item.subId.Equals(deviceId)).Select(t => t.measurement).FirstOrDefault());

            return Json(new { data = telemetry2, labels = telemetry1, device = deviceId, unitType = unitType });
        }
    }
}