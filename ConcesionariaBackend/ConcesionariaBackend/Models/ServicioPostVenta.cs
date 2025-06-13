using ConcesionariaBackend.Models;

public class ServicioPostVenta
{
    public int Id { get; set; }
    public string TipoServicio { get; set; } = null!;
    public DateTime? Fecha { get; set; }
    public string? Estado { get; set; }
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public int VehiculoId { get; set; }
    public Vehiculo Vehiculo { get; set; } = null!;
}



