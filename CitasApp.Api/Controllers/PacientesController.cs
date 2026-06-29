using Microsoft.AspNetCore.Mvc;
using CitasApp.Api.Models;
using CitasApp.Api.Services;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly DataService _db = DataService.Instance;

        [HttpGet]
        public ActionResult GetAll() => Ok(_db.Pacientes);

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var p = _db.Pacientes.FirstOrDefault(x => x.Id == id);
            return p == null ? NotFound() : Ok(p);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Paciente paciente)
        {
            var lista = _db.Pacientes;
            paciente.Id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1;
            lista.Add(paciente);
            _db.GuardarPacientes(lista);
            return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, paciente);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Paciente paciente)
        {
            var lista = _db.Pacientes;
            var idx = lista.FindIndex(p => p.Id == id);
            if (idx == -1) return NotFound();

            paciente.Id = id;
            lista[idx] = paciente;
            _db.GuardarPacientes(lista);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var lista = _db.Pacientes;
            var item = lista.FirstOrDefault(p => p.Id == id);
            if (item == null) return NotFound();

            lista.Remove(item);
            _db.GuardarPacientes(lista);
            return NoContent();
        }
    }
}