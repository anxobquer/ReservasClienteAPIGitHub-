using ClienteApi.Interfaces;
using ClienteApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ClienteApi.Repository
{
    public class RepServicios : IAPIServicios
    {
        private static string _urlBaseApi = "";
        private static string _tokenApi = "";
        private readonly HttpClient _httpClient;

        public RepServicios()
        {
            IConfigurationRoot builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            _urlBaseApi = builder.GetSection("ConexionApi:UrlBase").Value;

            _httpClient = new();
            _httpClient.BaseAddress = new Uri(_urlBaseApi);
        }

        public async Task<List<ModSerServicios>> MostrarServicios()
        {
            List<ModSerServicios> modServicio = new();
            _tokenApi = await new RepHelperApiUsuarios().LoginApi();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenApi);
            HttpResponseMessage response = await _httpClient.GetAsync(_urlBaseApi + "/Servicios/MostrarServicios");

            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                modServicio = JsonConvert.DeserializeObject<List<ModSerServicios>>(respuesta);
                return modServicio;
            }
            else
            {
                return await Task.FromResult(modServicio);
            }
        }

        public async Task<bool> RegistrarServicio([FromForm] ModSerServicios modServicio)
        {
            _tokenApi = await new RepHelperApiUsuarios().LoginApi();
            try
            {
                using MultipartFormDataContent content = new()
                {
                    { new StringContent("0"), "SerCodigo" },
                    { new StringContent(modServicio.SerNombre), "SerNombre" },
                    { new StringContent(modServicio.SerDescripcion), "SerDescripcion" },
                    { new StringContent(Convert.ToString(modServicio.SerPrecio)), "SerPrecio" },
                    { new StreamContent(modServicio.Imagen.OpenReadStream()), "Imagen", modServicio.Imagen.FileName },
                    { new StringContent(Convert.ToString(modServicio.SerFechaCreacion)), "SerFechaCreacion"},
                    { new StringContent(Convert.ToString(modServicio.SerUltModificacion)), "SerUltModificacion"},
                    { new StringContent("Ruta"), "Ruta" },
                };

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenApi);

                HttpResponseMessage response = await _httpClient.PostAsync(_urlBaseApi + "/Servicios/RegistrarServicio", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ModSerServicios> BuscarServicio(string codigo)
        {
            ModSerServicios modServicio = new();
            _tokenApi = await new RepHelperApiUsuarios().LoginApi();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenApi);
            HttpResponseMessage response = await _httpClient.GetAsync(_urlBaseApi + "/Servicios/BuscarServicio/" + codigo);

            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                modServicio = JsonConvert.DeserializeObject<ModSerServicios>(respuesta);
                return modServicio;
            }
            else
            {
                return await Task.FromResult(modServicio);
            }
        }

        public async Task<bool> ActualizarServicio([FromForm] ModSerServicios modServicio)
        {
            _tokenApi = await new RepHelperApiUsuarios().LoginApi();
            try
            {
                using MultipartFormDataContent content = new()
                {
                    { new StringContent(Convert.ToString(modServicio.SerCodigo)), "SerCodigo" },
                    { new StringContent(modServicio.SerNombre), "SerNombre" },
                    { new StringContent(modServicio.SerDescripcion), "SerDescripcion" },
                    { new StringContent(Convert.ToString(modServicio.SerPrecio)), "SerPrecio" },
                    { new StreamContent(modServicio.Imagen.OpenReadStream()), "Imagen", modServicio.Imagen.FileName },
                    { new StringContent(Convert.ToString(modServicio.SerFechaCreacion)), "SerFechaCreacion"},
                    { new StringContent(Convert.ToString(modServicio.SerUltModificacion)), "SerUltModificacion"},
                    { new StringContent("Ruta"), "Ruta" },
                };

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenApi);

                HttpResponseMessage response = await _httpClient.PutAsync(_urlBaseApi + "/Servicios/ActualizarServicio", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> EliminarServicio(string codigo)
        {
            _tokenApi = await new RepHelperApiUsuarios().LoginApi();


            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenApi);

            HttpResponseMessage response = await _httpClient.DeleteAsync(_urlBaseApi + "/Servicios/EliminarServicio/" + codigo);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
