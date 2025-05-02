namespace ConcesionariaBackend.DTOs
{
    public class ReporteVentasPorClienteDTO
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int CantidadVentas { get; set; }
        public decimal MontoTotal { get; set; }
    }
}
