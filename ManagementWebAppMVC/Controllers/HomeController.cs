using Bachelor.Common;
using Bachelor.Common.DataModels;
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
        private readonly powerconContext _context;

        public HomeController(ILogger<HomeController> logger, ICosmosDBService cosmosDBService, powerconContext context)
        {
            _logger = logger;
            _cosmosDBService = cosmosDBService;
            _context = context;
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
            var allDevices = _context.Devices
                .Join(_context.UnitTypes, 
                d => d.UnitTypeId, 
                u => u.Id, 
                (d, u) => new DeviceViewModel 
                { 
                    id = d.Id.ToString(), 
                    text = d.FriendlyName, 
                    unitType = u.Unit 
                });
            return Json(allDevices.ToList());           
        }

        public async Task<IActionResult> GetAllTelemetryForDevice(string deviceId, string unitType) {
            var telemetry = await _cosmosDBService.GetItemsAsync<OpsDeviceTelemetry>(x => 
                x.measurements.Any(y => y.subId.Equals(deviceId)));

            var telemetry1 = telemetry.Select(x => x.timestamp);
            var telemetry2 = telemetry.Select(x => x.measurements.Where(item => 
                item.subId.Equals(deviceId)).Select(t => t.measurement).FirstOrDefault());

            return Json(new { data = telemetry2, labels = telemetry1, device = deviceId, unitType = unitType });
        }
    }
}