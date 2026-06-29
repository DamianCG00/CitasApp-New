using Microsoft.AspNetCore.Mvc;
using CitasApp.Api.Models;
using CitasApp.Api.Services;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly DataService _db = DataService.Instance;

        [HttpGet]
        public ActionResult GetAll() => Ok(_db.Citas);

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var c = _db.Citas.FirstOrDefault(x => x.Id == id);
            return c == null ? NotFound() : Ok(c);
        }

        [HttpGet("porpaciente/{pacienteId}")]
        public ActionResult PorPaciente(int pacienteId)
        {
            if (!_db.Pacientes.Any(p => p.Id == pacienteId))
                return NotFound("Paciente no encontrado");

            var citas = _db.Citas.Where(c => c.PacienteId == pacienteId).ToList();
            return citas.Count == 0 ? NotFound("Sin citas para este paciente") : Ok(citas);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Cita cita)
        {
            var lista = _db.Citas;
            cita.Id = lista.Count > 0 ? lista.Max(c => c.Id) + 1 : 1;
            lista.Add(cita);
            _db.GuardarCitas(lista);
            return CreatedAtAction(nameof(GetById), new { id = cita.Id }, cita);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var lista = _db.Citas;
            var item = lista.FirstOrDefault(c => c.Id == id);
            if (item == null) return NotFound();

            lista.Remove(item);
            _db.GuardarCitas(lista);
            return NoContent();
        }
    }
}