using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using Service.DTOs;
using Service.Interfaces;
using Service.Models;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        public AuthService()
        {
        }

        public async Task<bool> CreateUserWithEmailAndPasswordAsync(string email, string password, string nombre)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("Email, password o nombre no pueden ser nulos o vacíos.");
            }
            try
            {
                var UrlApi = Properties.Resources.UrlApi;
                var client = new HttpClient();

                // Primero, registrar en Firebase a través del endpoint auth
                var authEndpoint = ApiEndpoints.GetEndpoint("Login");
                var registerRequest = new RegisterDTO { Email = email, Password = password, Nombre = nombre };
                var authResponse = await client.PostAsJsonAsync($"{UrlApi}{authEndpoint}/register/", registerRequest);
                
                if (!authResponse.IsSuccessStatusCode)
                {
                    return false;
                }

                var authResult = await authResponse.Content.ReadAsStringAsync();
                GenericService<object>.jwtToken = authResult;

                // Luego, crear el usuario en nuestra base de datos
                var usuariosEndpoint = ApiEndpoints.GetEndpoint("Usuario");
                var newUser = new Usuario
                {
                    Nombre = nombre,
                    Email = email,
                    Password = password,
                    TipoRol = Service.Enums.TipoRolEnum.Alumno,
                    FechaRegistracion = DateTime.Now,
                    Dni = "00000000"
                };

                var dbResponse = await client.PostAsJsonAsync($"{UrlApi}{usuariosEndpoint}", newUser);
                return dbResponse.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear usuario: " + ex.Message);
            }
        }

        public async Task<string?> Login(LoginDTO? login)
        {
            if (login==null)
            {
                throw new ArgumentException("El objeto login no llego.");
            }
            try
            {
                var urlApi = Properties.Resources.UrlApi;
                var endpointAuth = ApiEndpoints.GetEndpoint("Login");
                var client = new HttpClient();
                var response = await client.PostAsJsonAsync($"{urlApi}{endpointAuth}/login/",login);
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    GenericService<object>.jwtToken = result;   
                    return null;
                }
                else
                {
                    //si no es exitoso, devuelvo el mensaje de error
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return errorContent;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al loguearse->: " + ex.Message);
            }
        }

        public async Task<bool> ResetPassword(LoginDTO? login)
        {
            if (login == null)
            {
                throw new ArgumentException("El objeto login no llego.");
            }
            try
            {
                var urlApi = Properties.Resources.UrlApi;
                var endpointAuth = ApiEndpoints.GetEndpoint("Login");
                var client = new HttpClient();
                var response = await client.PostAsJsonAsync($"{urlApi}{endpointAuth}/resetpassword/", login);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al resetear el password->: " + ex.Message);
            }
        }
    }
}
