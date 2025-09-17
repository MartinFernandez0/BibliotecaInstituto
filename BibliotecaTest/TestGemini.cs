using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables; // <-- Agrega este using
using Microsoft.Extensions.Configuration.Json;
using Service.DTOs;
using Service.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BibliotecaTest
{
    public class TestGemini
    {
        [Fact]
        public async Task TestObtenerResumenLibroIA()
        {
            //leemos la api key desde appsettings.json
            var configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables()
                  .Build();

            var apiKey = configuration["ApiKeyGemini"];
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key= " + apiKey;

            var prompt = $"Me puedes dar un resumen de 100 palabras como m�ximo del libro Sin Red: Nadal, Federer y la historia detr�s del duelo que cambi� el tenis";

            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(payload);
            using var client = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            var texto = doc.RootElement
               .GetProperty("candidates")[0]
               .GetProperty("content")
               .GetProperty("parts")[0]
               .GetProperty("text")
               .GetString();

            Console.WriteLine($"Respuesta de IA: {texto}");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestObtenerResumenLibroIADesdeServicio()
        {
            //leemos la api key desde appsettings.json
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

            var apiKey = configuration["ApiKeyGemini"];
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key= " + apiKey;

            var prompt = $"Me puedes dar un resumen de 100 palabras como m�ximo del libro Sin Red: Nadal, Federer y la historia detr�s del duelo que cambi� el tenis";
            var servicio = new GeminiService(configuration);
            var resultado = await servicio.GetPrompt(prompt);
            Console.WriteLine($"Respuesta de IA desde servicio: {resultado}");
            Assert.NotNull(resultado);

        }

        [Fact]
        public async Task TestReconocerPortadaGeminiController()
        {

            // Ruta de la imagen de prueba (debe existir en la carpeta del proyecto)
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Portada.jpg");
            Assert.True(File.Exists(imagePath), $"No se encontró la imagen de prueba: {imagePath}");

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:7000/"); // Cambia el puerto si tu backend usa otro

            // Si necesitas token:
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var form = new MultipartFormDataContent();
            using var imageStream = File.OpenRead(imagePath);
            var imageContent = new StreamContent(imageStream);
            imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            form.Add(imageContent, "Image", "portada_test.jpg");

            // Puedes agregar otros campos si BookCoverExtractionRequestDTO los requiere

            var response = await client.PostAsync("api/Gemini/ocr-portada", form);
            var result = await response.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode, $"Error en la API: {result}");

            // Deserializa el resultado
            var metadata = JsonSerializer.Deserialize<BookMetadataDTO>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.NotNull(metadata);
            Assert.False(string.IsNullOrWhiteSpace(metadata.Titulo));
            Assert.NotNull(metadata.Autores);
            Assert.NotNull(metadata.Editorial);
        }
    }
}