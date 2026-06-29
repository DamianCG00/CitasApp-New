using Microsoft.AspNetCore.Mvc;
using CitasApp.Api.Models;
using System.Text.Json;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "data", "pacientes.json");
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        private List<Paciente> Leer() =>
            System.IO.File.Exists(_path)
                ? JsonSerializer.Deserialize<List<Paciente>>(System.IO.File.ReadAllText(_path), _opts) ?? new()
                : new();

        private void Guardar(List<Paciente> lista) =>
            System.IO.File.WriteAllText(_path, JsonSerializer.Serialize(lista, _opts));

        [HttpGet]
        public ActionResult GetAll() => Ok(Leer());

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var p = Leer().FirstOrDefault(x => x.Id == id);
            return p == null ? NotFound() : Ok(p);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Paciente paciente)
        {
            var lista = Leer();
            paciente.Id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1;
            lista.Add(paciente);
            Guardar(lista);
            return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, paciente);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Paciente paciente)
        {
            var lista = Leer();
            var idx = lista.FindIndex(p => p.Id == id);
            if (idx == -1) return NotFound();

            paciente.Id = id;
            lista[idx] = paciente;
            Guardar(lista);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var lista = Leer();
            var item = lista.FirstOrDefault(p => p.Id == id);
            if (item == null) return NotFound();

            lista.Remove(item);
            Guardar(lista);
            return NoContent();
        }
    }
}