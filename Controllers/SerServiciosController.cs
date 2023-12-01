using Microsoft.AspNetCore.Mvc;
using ClienteApi.Interfaces;
using ClienteApi.Models;

namespace ClienteApi.Controllers
{
    public class SerServiciosController : Controller
    {
        private readonly IAPIServicios _apiServicios;
        private readonly IWebHostEnvironment _webHost;

        public SerServiciosController(IAPIServicios ApoServicios, IWebHostEnvironment env)
        {
            _apiServicios = ApoServicios;
            _webHost = env;
        }
        public async Task<ActionResult> Index()
        {
            List<ModSerServicios> model = await _apiServicios.MostrarServicios();
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> MostrarLista()
        {
            List<ModSerServicios> model = await _apiServicios.MostrarServicios();
            return View("_ListaServicios", model);
        }
        
        [HttpGet]
        public ActionResult NuevoServicio()
        {
            return View("_RegistrarServicio");
        }
       
        [HttpPost]
        public async Task<ActionResult> RegistrarServicio([FromForm] ModSerServicios modServicio)
        {
            string[] msj = new string[2];
            bool respuesta = false;
            try
            {
                if (modServicio.Imagen == null)
                {
                    var url = "/Imagenes/none.png";
                    var path = ObtenerImagen(url);
                    using var stream = System.IO.File.OpenRead(path);
                    modServicio.Imagen = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    respuesta = await _apiServicios.RegistrarServicio(modServicio);
                }
                else
                {
                    respuesta = await _apiServicios.RegistrarServicio(modServicio);
                }
            }
            catch (Exception ex)
            {
                msj[0] = "0";
                msj[1] = "Error inesperado al intentar guardar el registro. " + ex.Message;
                return Json(msj);
            }

            if (respuesta)
            {
                msj[0] = "1";
                msj[1] = "Servicio registrado con éxito.";
            }
            else
            {
                msj[0] = "0";
                msj[1] = "Error inesperado al intentar guardar el registro.";
            }
            return Json(msj);
        }

        [HttpGet]
        public async Task<ActionResult> BuscarServicio(string codigo)
        {
            ModSerServicios model = await _apiServicios.BuscarServicio(codigo);
            return View("_MostrarServicio", model);
        }

        [HttpGet]
        public async Task<ActionResult> RegistrarServicio()
        {
            List<ModSerServicios> model = await _apiServicios.MostrarServicios();
            return View("_ListaServicios", model);
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarServicio([FromForm] ModSerServicios modServicio)
        {
            string[] msj = new string[2];
            bool respuesta = false;
            try
            {
                if (modServicio.Imagen == null)
                {
                    var url = "/Imagenes/none.png";
                    var path = ObtenerImagen(url);
                    using var stream = System.IO.File.OpenRead(path);
                    modServicio.Imagen = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    respuesta = await _apiServicios.ActualizarServicio(modServicio);
                }
                else
                {
                    respuesta = await _apiServicios.ActualizarServicio(modServicio);
                }
            }
            catch (Exception ex)
            {
                msj[0] = "0";
                msj[1] = "Error inesperado al intentar actualizar el registro. " + ex.Message;
                return Json(msj);
            }

            if (respuesta)
            {
                msj[0] = "1";
                msj[1] = "Servicio actualizado con éxito.";
            }
            else
            {
                msj[0] = "0";
                msj[1] = "Error inesperado al intentar actualizar el registro.";
            }
            return Json(msj);
        }

        public string ObtenerImagen(string url)
        {
            return Path.Combine(_webHost.WebRootPath, url.TrimStart('/').Replace("/","\\"));
        }

        [HttpDelete]
        public async Task<ActionResult> EliminarServicio(string codigo)
        {
            string[] msj = new string[2];
            bool respuesta = await _apiServicios.EliminarServicio(codigo);
            if (respuesta)
            {
                msj[0] = "1";
                msj[1] = "Servicio Eliminado con éxito.";
            }
            else
            {
                msj[0] = "0";
                msj[1] = "Error inesperado al intentar Eliminar el servicio.";
            }
            return Json(msj);
        }
    }
}
