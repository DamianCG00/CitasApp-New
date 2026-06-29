using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculadoraController : ControllerBase
    {
        [HttpGet("{operacion}")]
        public ActionResult Calcular(string operacion, [FromQuery] double a, [FromQuery] double b)
        {
            double resultado;
            switch (operacion.ToLower())
            {
                case "suma": resultado = a + b; break;
                case "resta": resultado = a - b; break;
                case "multiplicacion": resultado = a * b; break;
                case "division":
                    if (b == 0) return BadRequest(new { error = "No se puede dividir entre cero." });
                    resultado = a / b; break;
                default:
                    return BadRequest(new { error = $"Operación {operacion} no reconocida." });
            }
            return Ok(new { operacion = operacion.ToLower(), a, b, resultado });
        }

        [HttpGet("imc")]
        public ActionResult CalcularIMC([FromQuery] double peso, [FromQuery] double altura)
        {
            if (altura <= 0) return BadRequest(new { error = "La altura debe ser mayor a 0." });
            double imc = peso / (altura * altura);
            string categoria = imc < 18.5 ? "Bajo peso" : imc < 25 ? "Peso normal" : imc < 30 ? "Sobrepeso" : "Obesidad";
            return Ok(new { peso, altura, imc = Math.Round(imc, 2), categoria });
        }

        [HttpGet("edad")]
        public IActionResult CalcularEdad([FromQuery] int anioNacimiento)
        {
            int edad = DateTime.Now.Year - anioNacimiento;
            if (edad < 0 || edad > 150)
                return BadRequest(new { error = "Año de nacimiento no válido." });
            return Ok(new { anioNacimiento, edadActual = edad });
        }
    }
}