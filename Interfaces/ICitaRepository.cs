
using CitaApp.Web.Models;

namespace CitaApp.Web.Interfaces
{
    public interface ICitaRepository
    {
        List<Cita> ObtenerTodos();
        List<Cita> ObtenerPorPaciente(int pacienteId);
    }
}