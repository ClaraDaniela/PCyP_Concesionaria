namespace ConcesionariaBackend.Models
{
    public class ServicioPostVenta
    {
        public int Id { get; set; } // idServicioPosventa
        public string TipoServicio { get; set; } = null!;
        public DateTime? Fecha { get; set; }
        public string? Estado { get; set; }
        public string? Descripcion { get; set; }

        public int ClienteId { get; set; } // idCliente
        public Cliente Cliente { get; set; } = null!;

        public int VehiculoId { get; set; } // idVehiculo
        public Vehiculo Vehiculo { get; set; } = null!;
    }
}

