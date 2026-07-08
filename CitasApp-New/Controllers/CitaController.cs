using Microsoft.AspNetCore.Mvc;
using CitasApp.Models;
using System.Text.Json;

namespace CitasApp.Controllers
{
    public class CitaController : Controller
    {
        private readonly string _pathCitas = Path.Combine(Directory.GetCurrentDirectory(), "data", "citas.json");
        private readonly string _pathPacientes = Path.Combine(Directory.GetCurrentDirectory(), "data", "pacientes.json");
        private readonly string _pathMedicos = Path.Combine(Directory.GetCurrentDirectory(), "data", "medicos.json");
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        private List<T> Leer<T>(string path) =>
            File.Exists(path)
                ? JsonSerializer.Deserialize<List<T>>(File.ReadAllText(path), _opts) ?? new()
                : new();

        private void Guardar<T>(string path, List<T> lista) =>
            File.WriteAllText(path, JsonSerializer.Serialize(lista, _opts));

        public IActionResult Index()
        {
            ViewBag.Pacientes = Leer<Paciente>(_pathPacientes);
            ViewBag.Medicos = Leer<Medico>(_pathMedicos);
            return View(Leer<Cita>(_pathCitas));
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            ViewBag.Pacientes = Leer<Paciente>(_pathPacientes);
            ViewBag.Medicos = Leer<Medico>(_pathMedicos);
            return View(Leer<Cita>(_pathCitas).Where(c => c.PacienteId == pacienteId).ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Cita cita)
        {
            var lista = Leer<Cita>(_pathCitas);
            cita.Id = lista.Count > 0 ? lista.Max(c => c.Id) + 1 : 1;
            lista.Add(cita);
            Guardar(_pathCitas, lista);
            return RedirectToAction("Index");
        }
    }
}
