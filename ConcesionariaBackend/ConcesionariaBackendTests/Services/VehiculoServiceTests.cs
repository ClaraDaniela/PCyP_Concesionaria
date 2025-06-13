using AutoMapper;
using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Models;
using ConcesionariaBackend.Repositories;
using ConcesionariaBackend.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConcesionariaBackend.Tests.Services
{
    [TestClass]
    public class VehiculoServiceTests
    {
        [TestMethod]
        public async Task DisponibilidadVehiculosAsync_DevuelveSoloVehiculosDisponibles_Y_MideTiempo()
        {
            // Arrange: datos simulados
            var vehiculosSimulados = new List<Vehiculo>
            {
                new Vehiculo { Id = 1, Marca = "Ford", Modelo = "Fiesta", Stock = 3, Sucursal = new Sucursal { idSucursal = 1, nombre = "Sucursal A" } },
                new Vehiculo { Id = 2, Marca = "Toyota", Modelo = "Corolla", Stock = 0, Sucursal = new Sucursal { idSucursal = 2, nombre = "Sucursal B" } },
                new Vehiculo { Id = 3, Marca = "Chevrolet", Modelo = "Onix", Stock = 1, Sucursal = new Sucursal { idSucursal = 3, nombre = "Sucursal C" } },
            };

            var repoMock = new Mock<IVehiculoRepository>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(vehiculosSimulados);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<List<VehiculoDTO>>(It.IsAny<List<Vehiculo>>()))
                .Returns((List<Vehiculo> source) =>
                    source.Select(v => new VehiculoDTO
                    {
                        Id = v.Id,
                        Marca = v.Marca,
                        Modelo = v.Modelo,
                        Stock = v.Stock,
                        idSucursal = v.Sucursal.idSucursal,
                        Sucursal = new SucursalDTO
                        {
                            idSucursal = v.Sucursal.idSucursal,
                            nombre = v.Sucursal.nombre
                        },
                        Disponible = v.Stock > 0
                    }).ToList()
                );

            var service = new VehiculoService(repoMock.Object, mapperMock.Object);

            // Act
            var stopwatch = Stopwatch.StartNew();
            var resultado = await service.DisponibilidadVehiculosAsync();
            stopwatch.Stop();

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsTrue(resultado.All(v => v.Stock > 0), "Solo deben estar los vehículos con stock mayor a cero");
            Assert.AreEqual(2, resultado.Count, "Solo dos vehículos tienen stock disponible");

            // Loguear tiempo de ejecución (en ms)
            Console.WriteLine($"Tiempo de ejecución: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
