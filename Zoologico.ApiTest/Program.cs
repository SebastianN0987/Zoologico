using System.Net.Http.Json;
using System.Net.Http; // Necesario para HttpClient
using System; // Necesario para Uri
using System.Threading.Tasks; // Usaremos async/await para prácticas modernas
using Newtonsoft.Json; // Usaremos el namespace de Newtonsoft.Json
using System.Collections.Generic; // Para List<T>

// Define los modelos necesarios, asumiendo que están en este namespace o importados
namespace Zoologico.Modelos
{
    // Asume la estructura de respuesta de la API
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    // Asume la estructura del objeto Especie
    public class Especie
    {
        public int Codigo { get; set; }
        public string NombreComun { get; set; }
        // ... otras propiedades si las hay
    }
}

namespace Zoologico.ApiTest
{
    internal class Program
    {
        // Se recomienda usar async/await para operaciones HTTP
        static async Task Main(string[] args)
        {
            // La URL de la API de especies
            const string rutaEspecies = "api/Especies";

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7011/");

            Console.WriteLine("--- 1. Obtener todas las especies (GET) ---");

            // --- 1. Obtener datos (GET) ---
            try
            {
                // Usar await para esperar la respuesta de forma asíncrona
                var response = await httpClient.GetAsync(rutaEspecies);
                response.EnsureSuccessStatusCode(); // Lanza excepción si el código de estado es de error

                var json = await response.Content.ReadAsStringAsync();

                // Deserialización de la lista de especies
                var especies = JsonConvert.DeserializeObject<Modelos.ApiResult<List<Modelos.Especie>>>(json);

                Console.WriteLine($"Especies obtenidas: {especies.Data?.Count ?? 0}");
                // Puedes iterar sobre 'especies.Data' aquí si lo necesitas

                Console.WriteLine("\n--- 2. Inserción de una nueva especie (POST) ---");

                // --- 2. Inserción de datos (POST) ---
                var nuevaEspecie = new Modelos.Especie()
                {
                    Codigo = 0, // El Código suele ser 0 o ignorado si la API lo genera
                    NombreComun = "Lobo Gris"
                };

                // Serialización del objeto a JSON
                var especieJson = JsonConvert.SerializeObject(nuevaEspecie);
                // Creación del contenido HTTP
                var content = new StringContent(especieJson, System.Text.Encoding.UTF8, "application/json");

                // Invocar al servicio web para insertar la nueva especie
                response = await httpClient.PostAsync(rutaEspecies, content);
                response.EnsureSuccessStatusCode();

                json = await response.Content.ReadAsStringAsync();

                // Deserialización de la especie creada (la API debería devolver el objeto con el ID/Código generado)
                var especieCreadaResult = JsonConvert.DeserializeObject<Modelos.ApiResult<Modelos.Especie>>(json);
                var especieCreada = especieCreadaResult.Data;

                Console.WriteLine($"Especie creada con Codigo: {especieCreada.Codigo} y NombreComun: {especieCreada.NombreComun}");

                Console.WriteLine("\n--- 3. Actualización de la especie (PUT) ---");

                // --- 3. Actualización de datos (PUT) ---
                // Se requiere el código generado en el paso anterior para actualizar
                especieCreada.NombreComun = "Lobo Gris Actualizado";

                // Serializar el objeto actualizado
                especieJson = JsonConvert.SerializeObject(especieCreada);
                content = new StringContent(especieJson, System.Text.Encoding.UTF8, "application/json");

                // PUT normalmente requiere el ID/Código en la URL. 
                // Asumiendo que la ruta de actualización es api/Especies/{Codigo}
                string rutaActualizar = $"{rutaEspecies}/{especieCreada.Codigo}";

                // Invocar al servicio web para actualizar
                response = await httpClient.PutAsync(rutaActualizar, content);
                response.EnsureSuccessStatusCode();

                json = await response.Content.ReadAsStringAsync();

                // Deserialización de la respuesta de la actualización
                var especieActualizadaResult = JsonConvert.DeserializeObject<Modelos.ApiResult<Modelos.Especie>>(json);

                Console.WriteLine($"Especie actualizada a NombreComun: {especieActualizadaResult.Data.NombreComun}");

                // --- 4. Eliminación de la especie (DELETE) (Opcional pero recomendado) ---
                Console.WriteLine("\n--- 4. Eliminación de la especie (DELETE) ---");

                string rutaEliminar = $"{rutaEspecies}/{especieCreada.Codigo}";
                response = await httpClient.DeleteAsync(rutaEliminar);
                response.EnsureSuccessStatusCode();

                Console.WriteLine($"Especie con Codigo: {especieCreada.Codigo} eliminada.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
                // Puedes inspeccionar inner exceptions o el contenido de la respuesta de error si es necesario
            }

            Console.WriteLine("\nPresione Enter para finalizar...");
            Console.ReadLine();
        }
    }
}