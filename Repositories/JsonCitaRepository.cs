using System.Text.Json;
using CitaApp.Web.Interfaces;
using CitaApp.Web.Models;

namespace CitaApp.Web.Repositories
{
    public class JsonCitaRepository : JsonFileStore<CitaJson>, ICitaRepository
    {
        public JsonCitaRepository(IWebHostEnvironment env) : base(env, "citas.json") { }

        public List<Cita> ObtenerTodos()
        {
            var citasJson = Leer();
            return citasJson.Select(MapearACita).ToList();
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId) =>
            ObtenerTodos().Where(c => c.PacienteId == pacienteId).ToList();

        private static Cita MapearACita(CitaJson c) => new()
        {
            Id = c.Id,
            PacienteId = c.PacienteId,
            MedicoId = c.MedicoId,
            Fecha = DateOnly.Parse(c.Fecha),
            Hora = TimeOnly.Parse(c.Hora),
            Motivo = c.Motivo,
            Estado = c.Estado
        };
    }
}