using Microsoft.Extensions.Configuration;
using Service.Interfaces;
using Service.Models.InstitutoApp;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class InstitutoAppService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient = new HttpClient();
        public static string? jwtToken = string.Empty;
        public InstitutoAppService (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Usuario> GetPrompt(string textPrompt)
        {
            if (string.IsNullOrEmpty(textPrompt))
            {
                throw new ArgumentException("El texto del prompt no puede ser nulo o vacío.", nameof(textPrompt));
            }
            try 
            {
                var UrlApi = _configuration["UrlApi"];
                var endpointGemini = ApiEndpoints.GetEndpoint("Gemini");
                
                var response = await _httpClient.GetAsync($"{UrlApi}{endpointGemini}/prompt/{textPrompt}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    // Deserializar el string JSON a un objeto Usuario
                    var usuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(result);
                    if (usuario == null)
                    {
                        throw new Exception("No se pudo deserializar la respuesta a Usuario.");
                    }
                    return usuario;
                }
                else
                {
                    throw new Exception($"Error en la respuesta de la API:{response.StatusCode} -{response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el prompt de Gemini." + ex.Message);
            }
        }
    }
}
