namespace ConcesionariaBackend.DTOs
{
    public class VentaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VehiculoId { get; set; }
        public DateTime FechaVenta { get; set; }
        public required string MetodoPago { get; set; }
        public decimal MontoTotal { get; set; }

    }
}
