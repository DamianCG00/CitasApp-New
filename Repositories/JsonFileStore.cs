// Repositories/JsonFileStore.cs
using System.Text.Json;

public abstract class JsonFileStore<T>
{
    private readonly string _path;
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    protected JsonFileStore(IWebHostEnvironment env, string fileName)
    {
        _path = Path.Combine(env.ContentRootPath, "data", fileName);
    }

    protected List<T> Leer()
    {
        if (!File.Exists(_path)) return new();
        var json = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new();
    }
}