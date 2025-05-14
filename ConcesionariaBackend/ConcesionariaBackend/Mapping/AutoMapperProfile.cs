using AutoMapper;
using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Models;

namespace ConcesionariaBackend.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Cliente
            CreateMap<Cliente, ClienteDTO>();
            CreateMap<ClienteDTO, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Vehículo
            CreateMap<Vehiculo, VehiculoDTO>();
            CreateMap<VehiculoDTO, Vehiculo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Venta
            CreateMap<Venta, VentaDTO>();
            CreateMap<VentaDTO, Venta>()
                .ForMember(dest => dest.IdVenta, opt => opt.Ignore());

            // Servicio postventa
            CreateMap<ServicioPostVenta, ServicioPostVentaDTO>();
            CreateMap<ServicioPostVentaDTO, ServicioPostVenta>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Factura
            CreateMap<Factura, FacturaDTO>();
            CreateMap<FacturaDTO, Factura>()
                .ForMember(dest => dest.IdFactura, opt => opt.Ignore());
        }
    }
}


