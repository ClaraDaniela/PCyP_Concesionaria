using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using ConcesionariaBackend.Services;
using ConcesionariaBackend.Repositories;
using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Models;
using System.Diagnostics;

namespace ConcesionariaBackend.Tests.Services
{
    [TestClass]
    public class VentaServiceTests
    {
        [TestMethod]
        public async Task GetAllAsync_WithManyVentas_ShouldReturnMappedDTOsQuickly()
           {
                // Arrange
                var mockRepo = new Mock<IVentaRepository>();
                var mockMapper = new Mock<IMapper>();

                // Creamos 1000 ventas reales
                var ventas = Enumerable.Range(1, 1000)
                    .Select(i => new Venta
                    {
                        IdVenta = i,
                        ClienteId = i + 100,
                        VehiculoId = i + 200,
                        Fecha = DateTime.Now.AddDays(-i),
                        Total = i * 100,
                        MetodoPago = "Efectivo"
                    })
                    .ToList();

                // Simulamos el resultado del mapeo
                var ventasDTO = ventas
                    .Select(v => new VentaDTO
                    {
                        IdVenta = v.IdVenta,
                        ClienteId = v.ClienteId,
                        VehiculoId = v.VehiculoId,
                        FechaVenta = v.Fecha ?? DateTime.MinValue,
                        MontoTotal = v.Total,
                        MetodoPago = v.MetodoPago
                    })
                    .ToList();

                // Mockeamos el repo y el mapper
                mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(ventas);
                mockMapper.Setup(m => m.Map<IEnumerable<VentaDTO>>(ventas)).Returns(ventasDTO);

                var service = new VentaService(mockRepo.Object, mockMapper.Object);

                var stopwatch = Stopwatch.StartNew();

                // Act
                var result = await service.GetAllAsync();

                stopwatch.Stop();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1000, result.Count());
                Assert.AreEqual(100, result.First().MontoTotal);
                Assert.AreEqual("Efectivo", result.First().MetodoPago);
                Assert.IsTrue(stopwatch.ElapsedMilliseconds < 1000, $"Se tardó demasiado: {stopwatch.ElapsedMilliseconds}ms");
            }

        }
    }

