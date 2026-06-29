using Microsoft.AspNetCore.Mvc;
using CitasApp.Api.Models;
using CitasApp.Api.Services;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicosController : ControllerBase
    {
        private readonly DataService _db = DataService.Instance;

        [HttpGet]
        public ActionResult GetAll() => Ok(_db.Medicos);

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var m = _db.Medicos.FirstOrDefault(x => x.Id == id);
            return m == null ? NotFound() : Ok(m);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Medico medico)
        {
            var lista = _db.Medicos;
            medico.Id = lista.Count > 0 ? lista.Max(m => m.Id) + 1 : 1;
            lista.Add(medico);
            _db.GuardarMedicos(lista);
            return CreatedAtAction(nameof(GetById), new { id = medico.Id }, medico);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Medico medico)
        {
            var lista = _db.Medicos;
            var idx = lista.FindIndex(m => m.Id == id);
            if (idx == -1) return NotFound();

            medico.Id = id;
            lista[idx] = medico;
            _db.GuardarMedicos(lista);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var lista = _db.Medicos;
            var item = lista.FirstOrDefault(m => m.Id == id);
            if (item == null) return NotFound();

            lista.Remove(item);
            _db.GuardarMedicos(lista);
            return NoContent();
        }
    }
}