namespace ConcesionariaBackend.Models
{
    public class Factura
    {
        public int IdFactura { get; set; } // idFactura
        public string? NumeroFactura { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Total { get; set; }
        public string? TipoFactura { get; set; } // 'A', 'B' o 'C'
        public string? CuitCliente { get; set; }
        public string? RazonSocialCliente { get; set; }
        public int VentaId { get; set; } // Venta_idVenta
        public Venta Venta { get; set; } = null!; // Propiedad de navegación
    }
}
