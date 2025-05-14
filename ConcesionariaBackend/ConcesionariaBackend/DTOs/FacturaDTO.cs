namespace ConcesionariaBackend.DTOs
{
    public class FacturaDTO
    {
        public int IdFactura { get; set; }
        public required string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public required string TipoFactura { get; set; } // 'A', 'B' o 'C'
        public required string CuitCliente { get; set; }
        public required string RazonSocialCliente { get; set; }
        public int VentaId { get; set; } // Relación con Venta
    }
}
