using Microsoft.AspNetCore.Mvc;
using CitasApp.Api.Models;
using System.Text.Json;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicosController : ControllerBase
    {
        private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "data", "medicos.json");
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        private List<Medico> Leer() =>
            System.IO.File.Exists(_path)
                ? JsonSerializer.Deserialize<List<Medico>>(System.IO.File.ReadAllText(_path), _opts) ?? new()
                : new();

        private void Guardar(List<Medico> lista) =>
            System.IO.File.WriteAllText(_path, JsonSerializer.Serialize(lista, _opts));

        [HttpGet]
        public IActionResult GetAll() => Ok(Leer());

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var m = Leer().FirstOrDefault(x => x.Id == id);
            return m == null ? NotFound() : Ok(m);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Medico medico)
        {
            var lista = Leer();
            medico.Id = lista.Count > 0 ? lista.Max(m => m.Id) + 1 : 1;
            lista.Add(medico);
            Guardar(lista);
            return CreatedAtAction(nameof(GetById), new { id = medico.Id }, medico);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Medico medico)
        {
            var lista = Leer();
            var idx = lista.FindIndex(m => m.Id == id);
            if (idx == -1) return NotFound();

            medico.Id = id;
            lista[idx] = medico;
            Guardar(lista);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var lista = Leer();
            var item = lista.FirstOrDefault(m => m.Id == id);
            if (item == null) return NotFound();

            lista.Remove(item);
            Guardar(lista);
            return NoContent();
        }
    }
}