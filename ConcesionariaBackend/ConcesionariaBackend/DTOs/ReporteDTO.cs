namespace ConcesionariaBackend.DTOs
{
    public class ReporteDTO
    {
        public string ClienteNombre { get; set; } = null!;
        public int CantidadVentas { get; set; }
        public decimal MontoTotalVentas { get; set; }
        public int CantidadServicios { get; set; }
        public decimal MontoTotalServicios { get; set; }
        public DateTime FechaGeneracion { get; set; }
    }
}

