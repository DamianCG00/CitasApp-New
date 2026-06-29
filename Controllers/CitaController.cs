using CitaApp.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CitaApp.Web.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaRepository _citaRepo;
        private readonly IPacienteRepository _pacienteRepo;
        private readonly IMedicoRepository _medicoRepo;
        private readonly string _citaPath;
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        public CitaController(ICitaRepository citaRepo, IPacienteRepository pacienteRepo, IMedicoRepository medicoRepo, IWebHostEnvironment env)
        {
            _citaRepo = citaRepo;
            _pacienteRepo = pacienteRepo;
            _medicoRepo = medicoRepo;
            _citaPath = Path.Combine(env.ContentRootPath, "data", "citas.json");
        }

        public IActionResult Index()
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(_citaRepo.ObtenerTodos());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CitaApp.Web.Models.CitaJson cita)
        {
            var lista = _citaRepo.ObtenerTodos().ToList();
            cita.Id = lista.Count > 0 ? lista.Max(c => c.Id) + 1 : 1;

            // Leer JSON para no perder el formato string
            var rawList = JsonSerializer.Deserialize<List<CitaApp.Web.Models.CitaJson>>(System.IO.File.ReadAllText(_citaPath), _opts) ?? new();
            rawList.Add(cita);
            System.IO.File.WriteAllText(_citaPath, JsonSerializer.Serialize(rawList, _opts));

            return RedirectToAction("Index");
        }
    }
}