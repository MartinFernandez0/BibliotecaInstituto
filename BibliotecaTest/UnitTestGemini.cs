using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Service.DTOs;
using Service.Services;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.IO;
using Xunit;

namespace BiblioTestProject
{
    public class UnitTestGemini
    {
        [Fact]
        public async Task TestObtenerResumenLibroIA()
        {
            //leemos la clave de la api desde appsettings.json
            await LoginTest();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var apiKey = configuration["ApiKeyGemini"];
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key= " + apiKey;

            var prompt = $"Me puedes dar un resumen de 100 palabras como máximo de libro Mi Planta de Naranja lima";

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


        private async Task LoginTest()
        {
            var serviceAuth = new AuthService();
            var token = await serviceAuth.Login(new LoginDTO { Username = "martinexefe@gmail.com", Password = "123456" });
            Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>>>>>>Token: {token}");
            
        }



        [Fact]
        public async Task TestServiceGeminiGetPrompt()
        {
            await LoginTest();
            //leemos la api key desde appsettings.json
            var configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables()
                  .Build();
            var prompt = $"Me puedes dar un resumen de 100 palabras como máximo del libro Sin Red: Nadal, Federer y la historia detrás del duelo que cambió el tenis";
            var servicio = new GeminiService(configuration);
            var resultado = await servicio.GetPrompt(prompt);
            Console.WriteLine($"Respuesta de IA desde servicio: {resultado}");
            Assert.NotNull(resultado);


        }

        [Fact]
        public async Task TestReconocerPortadaGeminiController()
        {
            // Autenticación (si tu API requiere token, obténlo aquí)
            await LoginTest();

            // Ruta de la imagen de prueba (debe existir en la carpeta del proyecto)
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "portada_test.jpg");
            Assert.True(File.Exists(imagePath), $"No se encontró la imagen de prueba: {imagePath}");

            using var client = new HttpClient();
            //client.BaseAddress = new Uri("https://localhost:7000/"); // Cambia el puerto si tu backend usa otro

            // Si necesitas token:
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var form = new MultipartFormDataContent();
            using var imageStream = File.OpenRead(imagePath);
            var imageContent = new StreamContent(imageStream);
            imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            form.Add(imageContent, "Image", "portada_test.jpg");

            // Puedes agregar otros campos si BookCoverExtractionRequestDTO los requiere
            if (!string.IsNullOrEmpty(GenericService<object>.jwtToken))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenericService<object>.jwtToken);
            else
                throw new ArgumentException("Error Token no definido", nameof(GenericService<object>.jwtToken));

            var response = await client.PostAsync("https://localhost:7000/api/gemini/ocr-portada", form);
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