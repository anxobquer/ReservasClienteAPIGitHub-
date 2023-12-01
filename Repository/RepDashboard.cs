using ClienteApi.Interfaces;
using ClienteApi.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ClienteApi.Repository
{
    public class RepDashboard: IAPIDashboard
    {
        private static string _urlBaseApi = "";
        private static string _tokenApi = "";
        private readonly HttpClient _httpClient;

        public RepDashboard()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            _urlBaseApi = builder.GetSection("ConexionApi:UrlBase").Value;

            _httpClient = new();
            _httpClient.BaseAddress = new Uri(_urlBaseApi);
        }

        public async Task<ModDashboard> MostrarEstadisticas()
        {
            ModDashboard modelo = new();
            _tokenApi = await new RepHelperApiUsuarios().LoginApi();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenApi);
            HttpResponseMessage response = await _httpClient.GetAsync(_urlBaseApi + "/Estadisticas/MostrarEstadisticas");

            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync();
                modelo = JsonConvert.DeserializeObject<ModDashboard>(respuesta);
                return modelo;
            }
            else
            {
                return await Task.FromResult(modelo);
            }
        }
    }
}
