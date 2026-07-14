using System.Text.Json;
using CitaApp.Web.Interfaces;
using CitaApp.Web.Models;

namespace CitaApp.Web.Repositories
{
    public class JsonMedicoRepository : JsonFileStore<Medico>, IMedicoRepository
    {
        public JsonMedicoRepository(IWebHostEnvironment env) : base(env, "medicos.json") { }

        public List<Medico> ObtenerTodos() => Leer();

        public Medico? ObtenerPorId(int id) =>
            ObtenerTodos().FirstOrDefault(m => m.Id == id);
    }
}