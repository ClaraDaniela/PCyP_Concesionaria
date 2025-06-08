using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Models;
using ConcesionariaBackend.Services;
using ConcesionariaBackend.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConcesionariaBackend.Tests.Services
{
    [TestClass]
    public class VehiculoServiceTests
    {
        private List<Vehiculo> ObtenerVehiculosDePrueba() => new()
        {
            new Vehiculo { Id = 1, Marca = "Ford", Modelo = "Fiesta", Anio = 2018, Precio = 15000m, Color = "Rojo", Stock = 3 },
            new Vehiculo { Id = 2, Marca = "Toyota", Modelo = "Corolla", Anio = 2020, Precio = 22000m, Color = "Gris", Stock = 5 }
        };

        private Mock<IMapper> CrearMapperMock()
        {
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<List<VehiculoDTO>>(It.IsAny<List<Vehiculo>>()))
                .Returns((List<Vehiculo> sourceList) =>
                    sourceList.Select(v => new VehiculoDTO
                    {
                        Id = v.Id,
                        Marca = v.Marca ?? string.Empty,
                        Modelo = v.Modelo ?? string.Empty,
                        Anio = v.Anio,
                        Precio = v.Precio,
                        Color = v.Color ?? string.Empty,
                        Stock = v.Stock
                    }).ToList()
                );
            return mapperMock;
        }

        [TestMethod]
        public async Task SimularDisponibilidadVehiculosAsync()
        {
            // Arrange
            var vehiculos = ObtenerVehiculosDePrueba();
            var repoMock = new Mock<IVehiculoRepository>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(vehiculos);

            var mapperMock = CrearMapperMock();

            var service = new VehiculoService(repoMock.Object, mapperMock.Object);

            // Act
            var resultado = await service.SimularDisponibilidadVehiculosAsync();

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(6, resultado.Count, "Deberían devolverse 6 vehículos (2 vehículos * 3 sucursales simuladas)");

            // Verifica que el contenido mapeado sea coherente
            var primerVehiculo = resultado.FirstOrDefault();
            Assert.IsNotNull(primerVehiculo);
            Assert.AreEqual("Ford", primerVehiculo.Marca);
            Assert.AreEqual(2018, primerVehiculo.Anio);
            Assert.AreEqual(15000m, primerVehiculo.Precio);
        }

        [TestMethod]
        public async Task SimularDisponibilidadVehiculosAsync_Rendimiento()
        {
            // Arrange
            var vehiculos = ObtenerVehiculosDePrueba();
            var repoMock = new Mock<IVehiculoRepository>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(vehiculos);

            var mapperMock = CrearMapperMock();
            var service = new VehiculoService(repoMock.Object, mapperMock.Object);

            var stopwatch = new Stopwatch();

            const int numeroDePedidos = 100;
            var tareas = new List<Task<List<VehiculoDTO>>>();

            // Act
            stopwatch.Start();
            for (int i = 0; i < numeroDePedidos; i++)
            {
                tareas.Add(service.SimularDisponibilidadVehiculosAsync("Ford"));
            }
            await Task.WhenAll(tareas);
            stopwatch.Stop();

            // Assert 
            var resultado = tareas[0].Result;
            Assert.IsNotNull(resultado);
            Assert.IsTrue(resultado.All(v => v.Marca.Contains("Ford", StringComparison.OrdinalIgnoreCase)));

            // Output
            Debug.WriteLine($"⏱ Tiempo total para {numeroDePedidos} pedidos: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
