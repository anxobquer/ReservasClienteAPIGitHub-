using ClienteApi.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ClienteApi.Repository
{
    public class RepHelperApiUsuarios
    {
        private static string _usuarioApi = "";
        private static string _passwordApi = "";
        private static string _urlBaseApi = "";
        private readonly HttpClient _httpClient;

        public RepHelperApiUsuarios()
        {
            IConfigurationRoot builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            _usuarioApi = builder.GetSection("ConexionApi:UsuarioApi").Value;
            _passwordApi = builder.GetSection("ConexionApi:PasswordApi").Value;
            _urlBaseApi = builder.GetSection("ConexionApi:UrlBase").Value;

            _httpClient = new();
            _httpClient.BaseAddress = new Uri(_urlBaseApi);
        }

        public async Task<string> LoginApi()
        {
            ModLoginApi modLogin = new();
            modLogin.Usuario = _usuarioApi;
            modLogin.Pass = _passwordApi;

            StringContent content = new(JsonConvert.SerializeObject(modLogin), Encoding.UTF8, "Application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_urlBaseApi + "/token", content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
