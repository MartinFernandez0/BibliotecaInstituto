using Microsoft.Extensions.Configuration;
using Service.DTOs;
using Service.Models;
using Service.Services;

namespace BiblioTestProject
{
    public class UnitTestUsuarioService
    {
        //test GetAllAsync method of GenericService
        [Fact]
        public async Task Test_GetAllAsync_ReturnsListOfEntities()
        {
            // Arrange
            await LoginTest();
            var service = new UsuarioService();
            // Act
            var result = await service.GetAllAsync();
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Usuario>>(result);
            Assert.True(result.Count > 0);
        }

        private async Task LoginTest()
        {
            var serviceAuth = new AuthService();
            var token = await serviceAuth.Login(new LoginDTO { Username = "martinexefe@gmail.com", Password = "123456" });
            Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>>>>>>Token: {token}");
        }

        //test GetAllAsync method of GenericService
        [Fact]
        public async Task Test_GetByEmailAsync()
        {
            // Arrange
            await LoginTest();
            var service = new UsuarioService();
            // Act
            var result = await service.GetByEmailAsync("martinexefe@gmail.com");
            // Assert
            Assert.NotNull(result);
            Assert.IsType<Usuario>(result);
            Assert.Equal("martinexefe@gmail.com", result.Email);
        }




    }       
}
