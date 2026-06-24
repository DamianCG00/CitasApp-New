using CitaApp.Web.Models;

namespace CitaApp.Web.Interfaces
{
    public interface IPacienteRepository
    {
        List<Paciente> ObtenerTodos();
        Paciente? ObtenerPorId(int id);
    }
}