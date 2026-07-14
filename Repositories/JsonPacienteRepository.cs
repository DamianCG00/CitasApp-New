using System.Text.Json;
using CitaApp.Web.Interfaces;
using CitaApp.Web.Models;

namespace CitaApp.Web.Repositories
{
    public class JsonPacienteRepository : JsonFileStore<Paciente>, IPacienteRepository
    {
        public JsonPacienteRepository(IWebHostEnvironment env) : base(env, "pacientes.json") { }

        public List<Paciente> ObtenerTodos() => Leer();

        public Paciente? ObtenerPorId(int id) =>
            ObtenerTodos().FirstOrDefault(p => p.Id == id);
    }
}