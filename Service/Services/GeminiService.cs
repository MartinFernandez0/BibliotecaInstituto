using Microsoft.Extensions.Configuration;
using Service.Interfaces;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GeminiService : IGeminiService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient = new HttpClient();
        public static string? jwtToken = string.Empty;

        public GeminiService(IConfiguration configuration)
        {
            _configuration = configuration;
            if (!string.IsNullOrEmpty(GenericService<object>.jwtToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenericService<object>.jwtToken);
            else
                throw new ArgumentException("Error Token no definido", nameof(GenericService<object>.jwtToken));

        }
        public async Task<string?> GetPrompt(string textPrompt)
        {
            if (string.IsNullOrEmpty(textPrompt))
            {
                throw new ArgumentException("El texto del prompt no puede estar vacío.", nameof(textPrompt));
            }
            try
            {
                var urlApi = _configuration["UrlApi"];
                var endpointGemini = ApiEndpoints.GetEndpoint("Gemini");
                
                var response = await _httpClient.GetAsync($"{urlApi}{endpointGemini}/prompt/{textPrompt}");
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    throw new Exception($"Error en la respuesta de la API: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el prompt de Gemini: " + ex.Message);
            }




        }
    }
}
