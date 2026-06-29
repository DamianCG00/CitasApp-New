using CitaApp.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CitaApp.Web.Controllers
{
    public class MedicoController : Controller
    {
        private readonly IMedicoRepository _repo;
        private readonly string _path;
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        public MedicoController(IMedicoRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _path = Path.Combine(env.ContentRootPath, "data", "medicos.json");
        }

        public IActionResult Index() => View(_repo.ObtenerTodos());

        public IActionResult Detalle(int id)
        {
            var medico = _repo.ObtenerPorId(id);
            return medico == null ? NotFound() : View(medico);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CitaApp.Web.Models.Medico medico)
        {
            var lista = _repo.ObtenerTodos().ToList();
            medico.Id = lista.Count > 0 ? lista.Max(m => m.Id) + 1 : 1;
            lista.Add(medico);
            System.IO.File.WriteAllText(_path, JsonSerializer.Serialize(lista, _opts));
            return RedirectToAction("Index");
        }
    }
}