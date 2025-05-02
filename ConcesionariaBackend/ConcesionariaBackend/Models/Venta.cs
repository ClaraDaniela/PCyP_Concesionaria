using ConcesionariaBackend.Models;

public class Venta
{
    public int IdVenta { get; set; }
    public DateTime? Fecha { get; set; }
    public decimal? Total { get; set; }
    public string? MetodoPago { get; set; }

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public int VehiculoId { get; set; }
    public Vehiculo Vehiculo { get; set; } = null!;
    public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}


