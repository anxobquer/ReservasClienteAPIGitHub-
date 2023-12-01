
using ClienteApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClienteApi.Interfaces
{
    public interface IAPIDashboard
    {
        Task<ModDashboard> MostrarEstadisticas();

    }
}
