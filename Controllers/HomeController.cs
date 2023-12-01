using ClienteApi.Interfaces;
using ClienteApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClienteApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAPIService _apiService;

        public HomeController( IAPIService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            List<ModClieClientes> modClientes = await _apiService.MostrarClientes();
            return View(modClientes);
        }

        [HttpGet]
        public async Task<IActionResult> Regresar()
        {
            List<ModClieClientes> modClientes = await _apiService.MostrarClientes();
            return View("_listaClientes", modClientes);
        }
     
        [HttpGet]
        public async Task<IActionResult> BuscarCliente(string codigo)
        {
            ModClieClientes modClientes = await _apiService.BuscarCliente(codigo);
            return View("ActualizarCliente", modClientes);
        }

        [HttpGet]
        public IActionResult NuevoCliente()
        {
            return View("RegistrarCliente");
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCliente(ModClieClientes modClie)
        {
            bool respuesta = await _apiService.RegistrarCliente(modClie);
            string[] msj = new string[2];
            
            if (respuesta)
            {
                msj[0] = "1";
                msj[1] = "Cliente registrado con éxito.";
            }
            else
            {
                msj[0] = "0";
                msj[1] = "Error inesperado al intentar registrar el cliente.";
            }

            return Json(msj);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarCliente(ModClieClientes modClie)
        {
            bool respuesta = await _apiService.ActualizarCliente(modClie);
            string[] msj = new string[2];

            if (respuesta)
            {
                msj[0] = "1";
                msj[1] = "Cliente actualizado con éxito.";
            }
            else
            {
                msj[0] = "0";
                msj[1] = "Error inesperado al intentar actualizar el cliente.";
            }

            return Json(msj);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarCliente(string codigo)
        {
            bool respuesta = await _apiService.EliminarCliente(codigo);
            string[] msj = new string[2];

            if (respuesta)
            {
                msj[0] = "1";
                msj[1] = "Cliente eliminado con éxito.";
            }
            else
            {
                msj[0] = "0";
                msj[1] = "Error inesperado al intentar eliminar el cliente.";
            }

            return Json(msj);
        }
    }
}
