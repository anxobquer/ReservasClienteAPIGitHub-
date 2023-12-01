using ClienteApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClienteApi.Interfaces
{
    public interface IAPIService
    {
        Task<List<ModClieClientes>> MostrarClientes();
        Task<ModClieClientes> BuscarCliente(string codigo);
        Task<bool> RegistrarCliente([FromForm] ModClieClientes modClie);
        Task<bool> ActualizarCliente(ModClieClientes modClie);
        Task<bool> EliminarCliente(string codigo);

    }
}
