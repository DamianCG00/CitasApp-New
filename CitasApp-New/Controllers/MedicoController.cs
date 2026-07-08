using Microsoft.AspNetCore.Mvc;
using CitasApp.Models;
using System.Text.Json;

namespace CitasApp.Controllers
{
    public class MedicoController : Controller
    {
        private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "data", "medicos.json");
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        private List<Medico> Leer() =>
            File.Exists(_path)
                ? JsonSerializer.Deserialize<List<Medico>>(File.ReadAllText(_path), _opts) ?? new()
                : new();

        private void Guardar(List<Medico> lista) =>
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _opts));

        public IActionResult Index() => View(Leer());

        public IActionResult Detalle(int id)
        {
            var m = Leer().FirstOrDefault(x => x.Id == id);
            return m == null ? NotFound() : View(m);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Medico medico)
        {
            var lista = Leer();
            medico.Id = lista.Count > 0 ? lista.Max(m => m.Id) + 1 : 1;
            lista.Add(medico);
            Guardar(lista);
            return RedirectToAction("Index");
        }
    }
}
