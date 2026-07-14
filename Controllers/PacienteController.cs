using CitaApp.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CitaApp.Web.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteRepository _repo;
        private readonly string _path;
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        public PacienteController(IPacienteRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _path = Path.Combine(env.ContentRootPath, "data", "pacientes.json");
        }

        public IActionResult Index() => View(_repo.ObtenerTodos());

        public IActionResult Detalle(int id)
        {
            var paciente = _repo.ObtenerPorId(id);
            return paciente == null ? NotFound() : View(paciente);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CitaApp.Web.Models.Paciente paciente)
        {
            var lista = _repo.ObtenerTodos().ToList(); 
            paciente.Id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1;
            lista.Add(paciente);
            System.IO.File.WriteAllText(_path, JsonSerializer.Serialize(lista, _opts));
            return RedirectToAction("Index");
        }
    }
}