using CitasApp.Api.Models;
using System.Text.Json;

namespace CitasApp.Api.Services
{
    public sealed class DataService
    {
        // Instancia única (Singleton)
        private static readonly DataService _instance = new DataService();
        public static DataService Instance => _instance;

        private readonly string _pathP = Path.Combine(Directory.GetCurrentDirectory(), "data", "pacientes.json");
        private readonly string _pathM = Path.Combine(Directory.GetCurrentDirectory(), "data", "medicos.json");
        private readonly string _pathC = Path.Combine(Directory.GetCurrentDirectory(), "data", "citas.json");
        private readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        private DataService() { }

        public List<T> Leer<T>(string path) =>
            File.Exists(path) ? JsonSerializer.Deserialize<List<T>>(File.ReadAllText(path), _opts) ?? new() : new();

        public void Guardar<T>(string path, List<T> lista) =>
            File.WriteAllText(path, JsonSerializer.Serialize(lista, _opts));

        // Propiedades de acceso centralizado
        public List<Paciente> Pacientes => Leer<Paciente>(_pathP);
        public void GuardarPacientes(List<Paciente> l) => Guardar(_pathP, l);

        public List<Medico> Medicos => Leer<Medico>(_pathM);
        public void GuardarMedicos(List<Medico> l) => Guardar(_pathM, l);

        public List<Cita> Citas => Leer<Cita>(_pathC);
        public void GuardarCitas(List<Cita> l) => Guardar(_pathC, l);
    }
}