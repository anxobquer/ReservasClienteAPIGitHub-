using ClienteApi.Interfaces;
using ClienteApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClienteApi.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAPIDashboard _apiService;

        public DashboardController(IAPIDashboard iApi)
        {
            _apiService = iApi;
        }
        public async Task<ActionResult> Index()
        {
            ModDashboard? model = await _apiService.MostrarEstadisticas();
            return View("Dashboard", model);
            //return View();
        }

        public async Task<ActionResult> DashboardLink()
        {
            ModDashboard? model = await _apiService.MostrarEstadisticas();
            return View("DashboardLink", model);
            //return View();
        }

        [HttpGet]
        public async Task<ActionResult> MostrarEstadisticas()
        {
            ModDashboard? model = await _apiService.MostrarEstadisticas();
            return View(model);
        }
    }

}
