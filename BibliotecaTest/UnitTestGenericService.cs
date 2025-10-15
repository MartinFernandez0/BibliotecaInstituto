using Microsoft.Extensions.Configuration;
using Service.DTOs;
using Service.Models;
using Service.Services;

namespace BiblioTestProject
{
    public class UnitTestGenericService
    {
        //test GetAllAsync method of GenericService
        [Fact]
        public async Task Test_GetAllAsync_ReturnsListOfEntities()
        {
            // Arrange
            await LoginTest();
            var service= new GenericService<Libro>();
            // Act
            var result = await service.GetAllAsync();
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Libro>>(result);
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
        public async Task Test_GetAllAsync_WithFilter()
        {
            // Arrange
            await LoginTest();
            var service = new GenericService<Libro>();
            // Act
            var result = await service.GetAllAsync("catedral");
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Libro>>(result);
            Assert.True(result.Count == 1);
            Assert.Equal("Conversación en La Catedral", result[0].Titulo);
        }

        //test AddAsync method of GenericService
        [Fact]
        public async Task Test_AddAsync_AddsEntity()
        {
            // Arrange
            await LoginTest();
            var service = new GenericService<Libro>();
            var newLibro = new Libro
            {
                Titulo = "Test Libro",
                Descripcion = "Descripcion del libro de prueba",
                EditorialId = 1,
                Paginas = 100,
                AnioPublicacion = 2024,
                Portada = "portada.jpg",
                Sinopsis = "Sinopsis del libro de prueba"
            };
            // Act
            var result = await service.AddAsync(newLibro);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<Libro>(result);
            Assert.Equal("Test Libro", result.Titulo);
        }
        //test DeleteAsync method of GenericService
        [Fact]
        public async Task Test_DeleteAsync_DeletesEntity()
        {
            // Arrange
            await LoginTest();
            var service = new GenericService<Libro>();
            // First, add a new entity to ensure there is something to delete
            var newLibro = new Libro
            {
                Titulo = "Libro to Delete",
                Descripcion = "Descripcion del libro a eliminar",
                EditorialId = 1,
                Paginas = 150,
                AnioPublicacion = 2023,
                Portada = "portada_delete.jpg",
                Sinopsis = "Sinopsis del libro a eliminar"
            };
            var addedLibro = await service.AddAsync(newLibro);
            Assert.NotNull(addedLibro);
            // Act
            var result = await service.DeleteAsync(addedLibro.Id);
            // Assert
            Assert.True(result);
        }
        // test deleteds method of GenericService
        [Fact]
        public async Task Test_GetAllDeletedsAsync_ReturnsListOfDeletedEntities()
        {
            // Arrange
            await LoginTest();
            var service = new GenericService<Libro>();
            // Act
            var result = await service.GetAllDeletedsAsync();
            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Libro>>(result);
            Assert.True(result.Count >= 0); // Assuming there could be zero or more deleted entities
        }
        // update test method of GenericService
        [Fact]
        public async Task Test_UpdateAsync_UpdatesEntity()
        {
            // Arrange
            await LoginTest();
            var service = new GenericService<Libro>();
            // First, add a new entity to ensure there is something to update
            var newLibro = new Libro
            {
                Titulo = "Libro to Update",
                Descripcion = "Descripcion del libro a actualizar",
                EditorialId = 1,
                Paginas = 200,
                AnioPublicacion = 2022,
                Portada = "portada_update.jpg",
                Sinopsis = "Sinopsis del libro a actualizar"
            };
            var addedLibro = await service.AddAsync(newLibro);
            Assert.NotNull(addedLibro);
            // Modify some properties
            addedLibro.Titulo = "Updated Libro Title";
            addedLibro.Paginas = 250;
            // Act
            var result = await service.UpdateAsync(addedLibro);
            // Assert
            Assert.NotNull(result);
            Assert.True(result);
        }
        //test GetByIdAsync method of GenericService
        [Fact]
        public async Task Test_GetByIdAsync_ReturnsEntity()
        {
            // Arrange
            await LoginTest();
            var service = new GenericService<Libro>();
            // First, add a new entity to ensure there is something to get by ID
            var newLibro = new Libro
            {
                Titulo = "Libro to GetById",
                Descripcion = "Descripcion del libro a obtener por ID",
                EditorialId = 1,
                Paginas = 300,
                AnioPublicacion = 2021,
                Portada = "portada_getbyid.jpg",
                Sinopsis = "Sinopsis del libro a obtener por ID"
            };
            var addedLibro = await service.AddAsync(newLibro);
            Assert.NotNull(addedLibro);
            // Act
            var result = await service.GetByIdAsync(addedLibro.Id);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<Libro>(result);
            Assert.Equal("Libro to GetById", result.Titulo);
        }

        //restore deleted entity test method of GenericService
        [Fact]
        public async Task Test_RestoreDeletedAsync_RestoresEntity()
        {
            // Arrange
            await LoginTest();
            var service = new GenericService<Libro>();
            // First, add a new entity to ensure there is something to delete and then restore
            var newLibro = new Libro
            {
                Titulo = "Libro to Restore",
                Descripcion = "Descripcion del libro a restaurar",
                EditorialId = 1,
                Paginas = 350,
                AnioPublicacion = 2020,
                Portada = "portada_restore.jpg",
                Sinopsis = "Sinopsis del libro a restaurar"
            };
            var addedLibro = await service.AddAsync(newLibro);
            Assert.NotNull(addedLibro);
            // Delete the entity
            var deleteResult = await service.DeleteAsync(addedLibro.Id);
            Assert.True(deleteResult);
            // Act
            var restoreResult = await service.RestoreAsync(addedLibro.Id);
            // Assert
            Assert.True(restoreResult);
        }
    }
}
