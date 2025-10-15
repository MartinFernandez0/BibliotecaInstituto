using Microsoft.Extensions.Configuration;
using Service.DTOs;
using Service.Models;
using Service.Services;

namespace BiblioTestProject
{
    public class UnitTestAuthService
    {
        private async Task LoginTest()
        {
            var serviceAuth = new AuthService();
            var token = await serviceAuth.Login(new LoginDTO { Username = "martinexefe@gmail.com", Password = "123456" });
            Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>>>>>>Token: {token}");
        }

        [Fact]
        public async Task Reset_Password_Works_Correctly()
        {
            // Arrange
            await LoginTest();
            var serviceAuth = new AuthService();
            var loginDto = new LoginDTO
            {
                Username = "martinexefe@gmail.com",
                Password = "no hace falta en este proceso"
            };
            // Act
            var result = await serviceAuth.ResetPassword(loginDto);
            // Assert
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
