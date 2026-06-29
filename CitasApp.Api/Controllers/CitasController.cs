using Microsoft.AspNetCore.Mvc;
using CitasApp.Api.Models;
using System.Text.Json;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly string _pathC = Path.Combine(Directory.GetCurrentDirectory(), "data", "citas.json");
        private readonly string _pathP = Path.Combine(Directory.GetCurrentDirectory(), "data", "pacientes.json");
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        private List<T> Leer<T>(string path) =>
            System.IO.File.Exists(path)
                ? JsonSerializer.Deserialize<List<T>>(System.IO.File.ReadAllText(path), _opts) ?? new()
                : new();

        private void Guardar<T>(string path, List<T> lista) =>
            System.IO.File.WriteAllText(path, JsonSerializer.Serialize(lista, _opts));

        [HttpGet]
        public ActionResult GetAll() => Ok(Leer<Cita>(_pathC));

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var c = Leer<Cita>(_pathC).FirstOrDefault(x => x.Id == id);
            return c == null ? NotFound() : Ok(c);
        }

        [HttpGet("porpaciente/{pacienteId}")]
        public ActionResult PorPaciente(int pacienteId)
        {
            if (!Leer<Paciente>(_pathP).Any(p => p.Id == pacienteId))
                return NotFound("Paciente no encontrado");

            var citas = Leer<Cita>(_pathC).Where(c => c.PacienteId == pacienteId).ToList();
            return citas.Count == 0 ? NotFound("Sin citas para este paciente") : Ok(citas);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Cita cita)
        {
            var lista = Leer<Cita>(_pathC);
            cita.Id = lista.Count > 0 ? lista.Max(c => c.Id) + 1 : 1;
            lista.Add(cita);
            Guardar(_pathC, lista);
            return CreatedAtAction(nameof(GetById), new { id = cita.Id }, cita);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var lista = Leer<Cita>(_pathC);
            var item = lista.FirstOrDefault(c => c.Id == id);
            if (item == null) return NotFound();

            lista.Remove(item);
            Guardar(_pathC, lista);
            return NoContent();
        }
    }
}