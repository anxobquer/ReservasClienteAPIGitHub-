using ClienteApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClienteApi.Interfaces
{
    /// <summary>
    /// Interfaz que define las funcionalidades para el manto de los servicios.
    /// </summary>
    public interface IAPIServicios
    {
        // Mantenimiento de Servicios
        Task<List<ModSerServicios>> MostrarServicios();
        Task<ModSerServicios> BuscarServicio(string codigo);
        Task<bool> RegistrarServicio([FromForm] ModSerServicios modSerServicio);
        Task<bool> ActualizarServicio([FromForm] ModSerServicios modSerServicio);
        Task<bool> EliminarServicio(string codigo);
    }
}
