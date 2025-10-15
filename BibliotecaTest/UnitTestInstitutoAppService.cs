using Microsoft.Extensions.Configuration;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioTestProject
{
    public class UnitTestInstitutoAppService
    {
        //testeo obtener al usuario bridamore17@gmail.com
        //si no existe el usuario, el test debe fallar
        [Fact]
        public async Task TestGetUsuarioByEmailAsync()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            var institutoAppService = new InstitutoAppService(configuration);
            var email = "martinexefe@gmail.com";
            // Act
            var usuario = await institutoAppService.GetUsuarioByEmailAsync(email);
            // Assert
            Assert.NotNull(usuario);
            Assert.Equal(email, usuario.Email);
        }



        }
    }
