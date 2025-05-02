namespace ConcesionariaBackend.DTOs
{
    public class FacturaDTO
    {
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string TipoFactura { get; set; } // 'A', 'B' o 'C'
        public string CuitCliente { get; set; }
        public string RazonSocialCliente { get; set; }
        public int VentaId { get; set; } // Relación con Venta
    }
}
