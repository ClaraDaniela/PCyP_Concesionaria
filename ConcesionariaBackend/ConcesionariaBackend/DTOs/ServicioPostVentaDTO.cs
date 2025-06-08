namespace ConcesionariaBackend.DTOs
{
    public class ServicioPostVentaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VehiculoId { get; set; }
        public int VentaId { get; set; }
        public required string TipoServicio { get; set; } // Enum: Garantía, Mantenimiento, Reclamo
        public DateTime FechaSolicitud { get; set; }
        public required string Estado { get; set; } // Enum: Pendiente, En Proceso, Finalizado
        public required string Descripcion { get; set; }

    }
}
