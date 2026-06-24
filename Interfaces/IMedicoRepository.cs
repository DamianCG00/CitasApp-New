
using CitaApp.Web.Models;

namespace CitaApp.Web.Interfaces
{
    public interface IMedicoRepository
    {
        List<Medico> ObtenerTodos();
        Medico? ObtenerPorId(int id);
    }
}