using Microsoft.Extensions.Configuration;
using Service.DTOs;
using Service.Models;
using Service.Services;
using System.Text;
using System.Text.Json;

namespace BiblioTestProject
{
    public class UnitTestAuthService
    {
        [Fact]
        private async Task LoginTest()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceAuth = new AuthService();
            var token = await serviceAuth.Login(new LoginDTO
            {
                Username = "martinexefe@gmail.com",
                Password = "234567"
            });
        }

        [Fact]
        public async Task ResetPasswordTest()
        {
            await LoginTest();
            var serviceAuth = new AuthService();
            var loginDTO = new LoginDTO
            {
                Username = "martinexefe@gmail.com",
                Password = "no hace falta"

            };
            var result = await serviceAuth.ResetPassword(loginDTO);
            Assert.True(result);
        }

        [Fact]
        public async Task Create_User_Works_Correctly()
        {
            // Arrange
            await LoginTest();
            var serviceAuth = new AuthService();
            var random = new Random();
            var email = $"testuser{random.Next(1000, 9999)}@example.com";
            var password = "TestPassword123!";
            var nombre = "Test User";
            // Act
            var result = await serviceAuth.CreateUserWithEmailAndPasswordAsync(email, password, nombre);
            // Assert
            Assert.True(result);
        }
    }
}