using Microsoft.AspNetCore.Mvc;
using CitasApp.Models;
using System.Text.Json;

namespace CitasApp.Controllers
{
    public class PacienteController : Controller
    {
        private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "data", "pacientes.json");
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        private List<Paciente> Leer() =>
            File.Exists(_path)
                ? JsonSerializer.Deserialize<List<Paciente>>(File.ReadAllText(_path), _opts) ?? new()
                : new();

        private void Guardar(List<Paciente> lista) =>
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _opts));

        public IActionResult Index() => View(Leer());

        public IActionResult Detalle(int id)
        {
            var p = Leer().FirstOrDefault(x => x.Id == id);
            return p == null ? NotFound() : View(p);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Paciente paciente)
        {
            var lista = Leer();
            paciente.Id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1;
            lista.Add(paciente);
            Guardar(lista);
            return RedirectToAction("Index");
        }
    }
}
